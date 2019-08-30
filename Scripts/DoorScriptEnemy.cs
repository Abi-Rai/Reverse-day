using UnityEngine;
namespace Gamekit2D
{
    public class DoorScriptEnemy : MonoBehaviour
    {
        public static DoorScriptEnemy instance = null;
        public Animator doorAnim;
        public AnimatorStateInfo p_stateInfo;
        // Animator states stored into hash integers
        public int dOCStateHash = Animator.StringToHash("Base Layer.DOComplete");
        public int doorClosingHash = Animator.StringToHash("Base Laye.DoorClosing");
        public int doorIdleStateHash = Animator.StringToHash("Base Layer.DoorIdle");
        // Start is called before the first frame update
        void Start()
        {
            if (instance == null)
                instance = this;
            else if (instance != null)
                Destroy(gameObject);
            doorAnim = gameObject.GetComponent<Animator>();
        }
        void Update()
        {
            p_stateInfo = doorAnim.GetCurrentAnimatorStateInfo(0); // Current animator state stored
            //Debug.Log(("pstateINfO:"+p_stateInfo.fullPathHash));
            if (p_stateInfo.fullPathHash == dOCStateHash) EnemyManagerScript.instance.StartEnemySpawn();
            //if (p_stateInfo.fullPathHash == doorClosingHash) enemySpawnStop();

            //if (p_stateInfo.fullPathHash == doorIdleStateHash) EnemySpawner.instance.StopEnemySpawn();
        }
        public void OpenDoorAnim()
        {
            doorAnim.SetTrigger("SpawnEnemy");
        }
        public void CloseDoorAnim()
        {
            doorAnim.SetTrigger("StopSpawn");
            enemySpawnStop();
        }
        void enemySpawnStop()
        {
            EnemyManagerScript.instance.StopEnemySpawn();
        }
    }
}
