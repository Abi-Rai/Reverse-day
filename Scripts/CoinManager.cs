using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Gamekit2D
{

    public class CoinManager : MonoBehaviour, IDataPersister
    {
        [Serializable]

        public class ScoreEvent : UnityEvent<CoinManager>
        { }

        Vector2 playerPos;

        //private CharacterController2D charCont;

        public static CoinManager instance = null; // make game controller available in other scripts 

        GameObject coin2d;
        GameObject dropCoin;
        public Vector2 thrustC;
        public ScoreEvent OnScoreSet;
        //public int coinDecaytime; //(1) testing time delayed coin destroy 

        public int m_playerScore;
        public int PlayerScore
        {
            get { return m_playerScore; }
        }
        [HideInInspector]
        public DataSettings dataSettings;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(transform.gameObject);
            }
            else if (instance != null)
                Destroy(gameObject);
            coin2d = Resources.Load("GBP") as GameObject; // the coin prefab that drops for player to collect
            dropCoin = Resources.Load("DROPGBP") as GameObject; // the coin prefab that drops from the player and -- score
        }


        void OnEnable()
        {
            PersistentDataManager.RegisterPersister(this);
            m_playerScore = PlayerScore;
            //MoneyUI.instance.CoinBagLoadToScore(m_playerScore);
            OnScoreSet.Invoke(this);
            //MoneyUI.instance.CoinBagLoadToScore(m_playerScore);
        }

        void OnDisable()
        {
            PersistentDataManager.UnregisterPersister(this);
        }
        private void Start()
        {
            MoneyUI.instance.CoinBagLoadToScore(PlayerScore);
        }

        private void Update()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            String sceneName = currentScene.name;
            if (sceneName != "GameEnd" && sceneName != "startMenu")
            {
                playerPos = GameObject.Find("Ellen").GetComponent<CharacterController2D>().m_CurrentPosition; // Finds the Ellen GameObject and acceses the position from charactercontroller2d
            }
        }

        public void CoinDestroy(GameObject passedObject)
        {
            Destroy(passedObject);
        }
        public void CoinCollect(int passedValue, GameObject passedObject)
        {
            m_playerScore += passedValue;
            AudioSource source = GetComponent<AudioSource>();
            source.Play();
            MoneyUI.instance.CoinBagAdd();
            Destroy(passedObject);
        }

        public void SpawnCoinDrop()
        {
            Vector2 coinDropPos = playerPos + new Vector2(0, 1);
            GameObject coinClone = Instantiate(dropCoin, coinDropPos, Quaternion.identity) as GameObject;
            Rigidbody2D velAdd = coinClone.GetComponent<Rigidbody2D>();

            velAdd.AddForce(transform.up * thrustC);
            //Physics2D.IgnoreCollision(coinClone.GetComponent<CircleCollider2D>(), GetComponent<CapsuleCollider2D>()); // testing coding of phyics2D           
        }

        public void PlayerCoinDrop(Vector2 coinDropPosition)
        {
            Vector2 pickUpCoinPos = coinDropPosition;
            GameObject playerCoin = Instantiate(coin2d, pickUpCoinPos, Quaternion.identity) as GameObject;
            Rigidbody2D velAdd = playerCoin.GetComponent<Rigidbody2D>();
            velAdd.AddForce(transform.up * thrustC);
        }
        public void ChangePlayerScore(int passedAmount)
        {
            m_playerScore += passedAmount;

            OnScoreSet.Invoke(this);
        }

        public DataSettings GetDataSettings()
        {
            return dataSettings;
        }

        public void SetDataSettings(string dataTag, DataSettings.PersistenceType persistenceType)
        {
            dataSettings.dataTag = dataTag;
            dataSettings.persistenceType = persistenceType;
        }

        public Data SaveData()
        {
            return new Data<int>(PlayerScore);
        }

        public void LoadData(Data data)
        {
            Data<int> scoreData = (Data<int>)data;
            m_playerScore = scoreData.value;
            OnScoreSet.Invoke(this);
        }

    }
}
