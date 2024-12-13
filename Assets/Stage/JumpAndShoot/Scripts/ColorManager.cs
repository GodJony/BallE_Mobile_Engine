using UnityEngine;
using Random = UnityEngine.Random;

namespace _2D_BUNDLE.JumpAndShoot.Scripts {
    public class ColorManager : MonoBehaviour {
    
        public static ColorManager Instance;
    
        private float  _hueValue;
    
    
        private void Awake()
        {
            Instance = this;
        }

    
        private void Start()
        {
            // initialize background color.
            InitBackgroundColor();
        }

    
        void InitBackgroundColor()
        {
            // set _hueValue to a random value
            _hueValue = Random.Range(0, 10) / 10.0f;
            ChangeBackgroundColor();
        }

    
        public void ChangeBackgroundColor()
        {
            // convert the _hueValue to the rgb value and
            // set the backgroundColor of the main camera 
            Camera.main.backgroundColor = Color.HSVToRGB(_hueValue, 0.6f, 0.8f);
        
            // increase the _hueValue (0 ~ 1)
            _hueValue += 0.1f;
            if (_hueValue >= 1)
            {
                _hueValue = 0;
            }
        }

    
        public float GetCurrentHue()
        {
            return _hueValue;
        }


    }
}
