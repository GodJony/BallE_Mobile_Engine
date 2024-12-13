using UnityEngine;

namespace _2D_BUNDLE.JumpAndShoot.Scripts {
    public class GetDisplayBound : MonoBehaviour {

        public static GetDisplayBound Instance;

        float _mapX = 100.0f;


        private float _left;
        private float _right;



        void Awake()
        {
            Instance = this;
        
            float vertExtent = Camera.main.orthographicSize;
            float horzExtent = vertExtent * Screen.width / Screen.height;

            float minX = horzExtent - _mapX / 2.0f;
            float maxX = _mapX / 2.0f - horzExtent;

            _left = maxX - 50;
            _right = minX + 50;
        }

        public float GetLeftWallPosition()
        {
            return _left;
        }
    
        public float GetRightWallPosition()
        {
            return _right;
        }

    }
}