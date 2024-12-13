using UnityEngine;
using Random = UnityEngine.Random;

namespace _2D_BUNDLE.Wave.Scripts {
    public class ColorManager : MonoBehaviour {
    
        public static ColorManager Instance;
    
        private float hueValue;


        private void Awake()
        {
            Instance = this;
        }

    
        private void Start()
        {
            hueValue = Random.Range(0, 10) / 10.0f;
        
            ChangeBackgroundColor();
        }
    
    
        public void ChangeBackgroundColor()
        {
            hueValue += 0.1f;
            if (hueValue >= 1)
            {
                hueValue = 0;
            }
            Camera.main.backgroundColor = Color.HSVToRGB(hueValue, 0.6f, 0.8f);
        }
    }
}
