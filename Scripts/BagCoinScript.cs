using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gamekit2D
{
    public class BagCoinScript : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            if (transform.position.y < -13)
            {
                MoneyUI.instance.CoinBagSpecificDelete(this.gameObject);
            }
        }
    }
}
