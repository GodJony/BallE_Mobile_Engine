using System;
using UnityEngine;

namespace _2D_BUNDLE.WallToWall2022.Scripts {
    public class BallActionManager : MonoBehaviour {
    
    
        public static BallActionManager Instance { get; private set; }

        public Action onBallGenerated;
        public Action onGameStarted;
        public Action onBallJumped;
        public Action onWallTouched;
        public Action onObstacleTouched;
        public Action onBallExplode;


        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void InvokeOnBallGenerated()
        {
            onBallGenerated?.Invoke();
        }
    
        public void InvokeOnGameStarted()
        {
            onGameStarted?.Invoke();
        }

        public void InvokeOnBallJumped()
        {
            onBallJumped?.Invoke();
        }

        public void InvokeOnWallTouched()
        {
            onWallTouched?.Invoke();
        }

        public void InvokeOnObstacleTouched()
        {
            onObstacleTouched?.Invoke();
        }
    
        public void InvokeOnBallExploded()
        {
            onBallExplode?.Invoke();
        }

    
    
    
    
    }
}
