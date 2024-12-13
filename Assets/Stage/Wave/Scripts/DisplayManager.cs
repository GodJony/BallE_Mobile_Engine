using UnityEngine;

namespace _2D_BUNDLE.Wave.Scripts {
    public class DisplayManager : MonoBehaviour
    {


        // -------------------------- Display Bound -------------------------- // 
        private float mapX = 100.0f;
        private float mapY = 100.0f;

        private float minX;
        private float maxX;
        private float minY;
        private float maxY;

        private Vector2 LEFT_UP;
        private Vector2 LEFT_DOWN;
        private Vector2 RIGHT_UP;
        private Vector2 RIGHT_DOWN;

        private float WIDTH;
        private float HEIGHT;

        private float LEFT;
        private float RIGHT;
        private float DOWN;
        private float UP;


        void Awake()
        {
            InitDisplayBound();
        }



        void InitDisplayBound()
        {
            float vertExtent = Camera.main.orthographicSize;
            float horzExtent = vertExtent * Screen.width / Screen.height;


            minX = horzExtent - mapX / 2.0f;
            maxX = mapX / 2.0f - horzExtent;
            minY = vertExtent - mapY / 2.0f;
            maxY = mapY / 2.0f - vertExtent;

            LEFT_UP = new Vector2(maxX - 50, minY + 50);
            LEFT_DOWN = new Vector2(maxX - 50, maxY - 50);
            RIGHT_UP = new Vector2(minX + 50, minY + 50);
            RIGHT_DOWN = new Vector2(minX + 50, maxY - 50);

            WIDTH = RIGHT_DOWN.x - LEFT_DOWN.x;
            HEIGHT = LEFT_UP.y - LEFT_DOWN.y;

            LEFT = LEFT_UP.x;
            RIGHT = RIGHT_UP.x;
            UP = LEFT_UP.y;
            DOWN = LEFT_DOWN.y;
        }

    
        public float GetWidth()
        {
            return WIDTH;
        }

    }
}