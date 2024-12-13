using UnityEngine;


// -------------------------- Get Display Info (width, height etc...) -------------------------- //

namespace _2D_BUNDLE.ColorJump.Scripts {
    public class DisplayManager : MonoBehaviour {
    
        public static DisplayManager Instance;

    
        private float mapX = 100.0f;
        private float mapY = 100.0f;

        private float minX;
        private float maxX;
        private float minY;
        private float maxY;

        private Vector2 leftTop;
        private Vector2 leftBottom;
        private Vector2 rightTop;
        private Vector2 rightBottom;

        private float width;
        private float height;
        private float left;
        private float right;
    
        private float bottom;
        private float top;


        void Awake()
        {
            Instance = this;
        
            InitDisplayBound();
        }



        // get screen width, height etc...
        void InitDisplayBound()
        {
            float vertExtent = Camera.main.orthographicSize;
            float horzExtent = vertExtent * Screen.width / Screen.height;


            minX = horzExtent - mapX / 2.0f;
            maxX = mapX / 2.0f - horzExtent;
            minY = vertExtent - mapY / 2.0f;
            maxY = mapY / 2.0f - vertExtent;

            leftTop = new Vector2(maxX - 50, minY + 50);
            leftBottom = new Vector2(maxX - 50, maxY - 50);
            rightTop = new Vector2(minX + 50, minY + 50);
            rightBottom = new Vector2(minX + 50, maxY - 50);

            width = rightBottom.x - leftBottom.x;
            height = leftTop.y - leftBottom.y;

            left = leftTop.x;
            right = rightTop.x;
            top = leftTop.y;
            bottom = leftBottom.y;
        }


        public float GetHeight()
        {
            return height;
        }

        public float GetLeft()
        {
            return left;
        }
        public float GetRight()
        {
            return right;
        }

        public float GetBottom()
        {
            return bottom;
        }
    

    }
}