using DG.Tweening;
using UnityEngine;

namespace _2D_BUNDLE.WallToWall2022.Scripts {
    public class CameraManager : MonoBehaviour {

        private Camera mainCamera;

        [SerializeField] private float jump_duration;
        [SerializeField] private float jump_strength;
        [SerializeField] private int jump_vibrato;
    
        [SerializeField] private float wallTouch_duration;
        [SerializeField] private float wallTouch_strength;
        [SerializeField] private int wallTouch_vibrato;
    
        [SerializeField] private float obstacleTouched_duration;
        [SerializeField] private float obstacleTouched_strength;
        [SerializeField] private int obstacleTouched_vibrato;
    
        [SerializeField] private float ballExplode_duration;
        [SerializeField] private float ballExplode_strength;
        [SerializeField] private int ballExplode_vibrato;
    
    
        private void Start()
        {
            mainCamera = Camera.main;
            BallActionManager.Instance.onBallJumped += OnBallJumped;
            BallActionManager.Instance.onWallTouched += OnWallTouched;
            BallActionManager.Instance.onObstacleTouched += OnObstacleTouched;
            BallActionManager.Instance.onBallExplode += OnBallExplode;

        }

   


        private void OnWallTouched()
        {
            ShakeCamera(wallTouch_duration, wallTouch_strength, wallTouch_vibrato);
        }


        private void OnBallJumped()
        {
            // ShakeCamera(jump_duration, jump_strength, jump_vibrato);
        }

    
        private void OnObstacleTouched()
        {
            ShakeCamera(obstacleTouched_duration, obstacleTouched_strength, obstacleTouched_vibrato);
        }
    
        private void OnBallExplode()
        {
            ShakeCamera(ballExplode_duration, ballExplode_strength, ballExplode_vibrato);

        }

    
        private void ShakeCamera(float duration, float strength, int vibrato)
        {
            mainCamera.transform.DOShakePosition(duration, strength, vibrato);
        }
    


    }
}
