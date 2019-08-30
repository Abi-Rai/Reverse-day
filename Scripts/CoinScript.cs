using UnityEngine;
namespace Gamekit2D
{
    public class CoinScript : MonoBehaviour
    {
        public int coinValue;
        Vector2 playerPos;
        private void Awake()
        {

        }
        private void OnCollisionEnter2D(Collision2D collision) // collison detection only occurs with player
        {
            //Debug.Log(collision);

            if (collision.gameObject.tag == "Player")
            {
                CoinManager.instance.CoinCollect(coinValue, gameObject);
            }
        }

        private void OnTriggerStay2D(Collider2D col) // only occurs with triggers
        {
            CoinManager.instance.CoinDestroy(gameObject);
        }
    }
}
