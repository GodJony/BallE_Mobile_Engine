using UnityEngine;

namespace _2D_BUNDLE.WallToWall2022.Scripts {
    public class SoundManager : MonoBehaviour {

        private AudioSource audioSource;

        [SerializeField] private AudioClip ballCreatedClip;
        [SerializeField] private AudioClip gameStartedClip;
        [SerializeField] private AudioClip jumpClip;
        [SerializeField] private AudioClip wallTouchedClip;
        [SerializeField] private AudioClip obstacleTouchedClip;
        [SerializeField] private AudioClip explodeClip;


        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            BallActionManager.Instance.onBallGenerated += OnBallGenerated;
            BallActionManager.Instance.onGameStarted += OnGameStarted;
            BallActionManager.Instance.onBallJumped += OnBallJumped;
            BallActionManager.Instance.onWallTouched += OnWallTouched;
            BallActionManager.Instance.onObstacleTouched += OnObstacleTouched;
            BallActionManager.Instance.onBallExplode += OnBallExplode;
        }

    

        private void OnBallGenerated()
        {
            audioSource.PlayOneShot(ballCreatedClip);

        }
    
        private void OnGameStarted()
        {
            audioSource.PlayOneShot(gameStartedClip);

        }


        private void OnBallJumped()
        {
            audioSource.PlayOneShot(jumpClip);
        }


        private void OnWallTouched()
        {
            audioSource.PlayOneShot(wallTouchedClip);
        }
    
        private void OnObstacleTouched()
        {
            audioSource.PlayOneShot(obstacleTouchedClip);

        }
    
        private void OnBallExplode()
        {
            audioSource.PlayOneShot(explodeClip);

        }
    
    
    }
}