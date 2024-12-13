using UnityEngine;


// todo 오디오 클립 추가
namespace _2D_BUNDLE.LeftOrRight.Scripts {
    public class AudioManager : MonoBehaviour {
    
    
        [SerializeField] private AudioSource audioSource;
    
        // audio clip
        [SerializeField] private AudioClip moveClip;
        [SerializeField] private AudioClip itemClip;
        [SerializeField] private AudioClip outClip;
        [SerializeField] private AudioClip explodeClip;
    
    
        private void Start()
        {
            ActionManager.onPlayerMoved += OnPlayerMoved;
            ActionManager.onPlayerGetItem += OnPlayerGetItem;
            ActionManager.onPlayerOut += OnPlayerOut ;
            ActionManager.onPlayerExploded += OnPlayerExploded;
        }


        private void OnDestroy()
        {
            ActionManager.onPlayerMoved -= OnPlayerMoved;
            ActionManager.onPlayerGetItem -= OnPlayerGetItem;
            ActionManager.onPlayerOut -= OnPlayerOut ;
            ActionManager.onPlayerExploded -= OnPlayerExploded;
        }


        private void OnPlayerMoved()
        {
            audioSource.PlayOneShot(moveClip, 1);
        }

        private void OnPlayerGetItem(int itemTime)
        {
            audioSource.PlayOneShot(itemClip, 1);
        }

        private void OnPlayerOut()
        {
            audioSource.PlayOneShot(outClip, 1);
        }

        private void OnPlayerExploded()
        {
            audioSource.PlayOneShot(explodeClip, 1);
        }
    
    
    

    }
}


