using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Gamekit2D
{
    public class NextLevelScript : MonoBehaviour
    {
        public UnityEvent onTriggerEnter;
        public UnityEvent onInterectPress;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject == PlayerCharacter.PlayerInstance.gameObject)
            {
                onTriggerEnter.Invoke();
            }
        }
        private void Update()
        {
            if (PlayerInput.Instance.Interact.Down)
            {
                onInterectPress.Invoke();
            }
        }
        public void LoadLevel2()
        {
            SceneManager.LoadScene("Level2");               
            
        }
        public void LoadLevel1()
        {
            SceneManager.LoadScene("Level1");
        }
        public void LoadEndLevel()
        {
            SceneManager.LoadScene("GameEnd");
        }
    }
}