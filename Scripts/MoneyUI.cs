using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Gamekit2D
{
    public class MoneyUI : MonoBehaviour
    {
        public static MoneyUI instance = null; // make available in other scripts 
        GameObject bagCoin2d;
        List<GameObject> bagCoinList;
        public GameObject moneyBag;
        //TextMeshPro components
        public TextMeshProUGUI bagCoinScore;
        public GameObject scoreText;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(transform.gameObject);
            }
            else if (instance != null)
                Destroy(gameObject);
            bagCoin2d = Resources.Load("BagCoin") as GameObject; // loading coin prefab
            bagCoinList = new List<GameObject>();
        }
        private void Update()
        {
            bagCoinScore.text = CoinManager.instance.m_playerScore.ToString();
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
            if (sceneName == "GameEnd")
            {
                gameObject.GetComponentInChildren<Camera>().enabled = false;
            }
            if (sceneName == "Level1")
            {
                gameObject.GetComponentInChildren<Camera>().enabled = true;
            }
        }
        public void CoinBagAdd()
        {
            //Final setup for canvas money bag
            Vector2 randomCoinPos = moneyBag.transform.position + (new Vector3(Random.Range(-14f, 14f), 45f, 0f));
            // changed to using list
            bagCoinList.Add(Instantiate(bagCoin2d, randomCoinPos, Quaternion.identity));
            foreach (GameObject setChild in bagCoinList)
            {
                setChild.transform.parent = moneyBag.transform;
                //setChild.transform.position = cloneCoinPos;
                setChild.transform.localScale = new Vector3(0.06405383f, 0.06405383f, 0.06405383f);
            }
        }
        public void CoinBagDelete()
        {
            //GameObject coinToDelete = bagCoinList[0]; // to delete first to last
            GameObject coinToDelete = bagCoinList[bagCoinList.Count - 1];// to delete last to first
            bagCoinList.Remove(coinToDelete);
            Destroy(coinToDelete);
            CoinManager.instance.ChangePlayerScore(-1);
        }
        public void CoinBagSpecificDelete(GameObject coDel)
        {
            bagCoinList.Remove(coDel);
            Destroy(coDel);
            CoinManager.instance.ChangePlayerScore(-1);
            CoinManager.instance.SpawnCoinDrop();
        }
        public void CoinBagLoadToScore(int p_score)
        {
            for (int i = 0; i < p_score; i++)
            {
                Vector2 randomCoinPos = moneyBag.transform.position + (new Vector3(Random.Range(-12f, 12f), (Random.Range(45f, 40f)), 0f));

                bagCoinList.Add(Instantiate(bagCoin2d, randomCoinPos, Quaternion.identity));
            }
            foreach (GameObject setChild in bagCoinList)
            {
                setChild.transform.parent = moneyBag.transform;
                //setChild.transform.position = cloneCoinPos;
                setChild.transform.localScale = new Vector3(0.06405383f, 0.06405383f, 0.06405383f);
            }
        }

        public void CoinBagDeleteAmount(int p_amount)
        {
            for (int i = 0; i < p_amount; i++)
            {
                GameObject coinToDelete = bagCoinList[bagCoinList.Count - 1];// to delete last to first
                bagCoinList.Remove(coinToDelete);
                Destroy(coinToDelete);
            }
        }
    }
}
