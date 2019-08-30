using UnityEngine;

namespace Gamekit2D
{
    public class MusicChangeScript : MonoBehaviour
    {
        public GameObject BGmusicPLayer;
        public AudioClip BossMusic;
        public AudioClip NormalMusic;


        // Update is called once per frame
        void Update()
        {
            BGmusicPLayer = GameObject.Find("BackgroundMusicPlayer");
        }

        public void ChangeMusicToBoss()
        {
            BGmusicPLayer.GetComponent<BackgroundMusicPlayer>().ChangeMusic(BossMusic);
        }
        public void ChangeToNormal()
        {
            BGmusicPLayer.GetComponent<BackgroundMusicPlayer>().ChangeMusic(NormalMusic);
        }
    }
}