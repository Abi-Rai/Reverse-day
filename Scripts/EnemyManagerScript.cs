using System.Collections;
using UnityEngine;

namespace Gamekit2D
{
    public class EnemyManagerScript : MonoBehaviour
    {

        public int m_currentLevel;
        public bool levelStart;
        public float spawnDelayAfterPad; // number of seconds until the first enemy door opens
        public Light pointLight;
        public GameObject level1Spawner;
        public GameObject level2spawner;

        public static EnemyManagerScript instance = null; // make available in other scripts
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != null)
                Destroy(gameObject);
        }
        // The initial method to start enemy spawn
        public void StartFirstSpawn()
        {
            StartCoroutine(WaitAndSpawn(spawnDelayAfterPad));
        }
        private IEnumerator WaitAndSpawn(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                DoorScriptEnemy.instance.OpenDoorAnim();
                if (pointLight != null) pointLight.GetComponent<Light>().enabled = true;
            }
        }
        public void StartEnemySpawn()
        {
            level1Spawner.GetComponent<EnemySpawner>().enabled = true;
            if (m_currentLevel == 2)
            {
                level2spawner.GetComponent<EnemySpawner>().enabled = true;
            }
        }

        public void StopEnemySpawn()
        {
            level1Spawner.GetComponent<EnemySpawner>().enabled = false;
            if (m_currentLevel == 2)
            {
                //level2spawner.GetComponent<EnemySpawner>().enabled = false;
                //level2spawner.GetComponent<EnemySpawner>().totalEnemiesToBeSpawned = 0;
            }
        }
    }
}