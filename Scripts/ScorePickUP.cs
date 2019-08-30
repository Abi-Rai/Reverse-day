using UnityEngine;
using UnityEngine.Events;

namespace Gamekit2D
{
    public class ScorePickUP : MonoBehaviour
    {
        public int scoreRequired = 8;
        public UnityEvent OnPickup;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject == PlayerCharacter.PlayerInstance.gameObject)
            {

                CoinManager coinManInst = CoinManager.instance;
                if (coinManInst.m_playerScore >= scoreRequired)
                {

                    coinManInst.ChangePlayerScore(scoreRequired * -1);
                    MoneyUI.instance.CoinBagDeleteAmount(scoreRequired);
                    OnPickup.Invoke();
                }
            }
        }
                //private void OnTriggerStay2D(Collider2D other)
        //{
        //    if(other.gameObject == PlayerCharacter.PlayerInstance.gameObject)
        //    {

        //    }
        //}

    }
}