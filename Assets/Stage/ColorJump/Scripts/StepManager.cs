using UnityEngine;
using Random = UnityEngine.Random;

namespace _2D_BUNDLE.ColorJump.Scripts {
    public class StepManager : MonoBehaviour {
    
        public static StepManager Instance;

        [SerializeField] private GameObject stepPrefab;

        [Space]
        [SerializeField] private float stepWidth; 
        [SerializeField] private float stepMin;
        [SerializeField] private float stepWidthDecrease;
        [SerializeField] private float _distanceToNextStep = 4;

    
        private float _hueValue;
        private int _stepIndex = 1;

    
        private void Awake()
        {
            Instance = this;
        }

    
        void Start()
        {
            //background Color Initialization
            InitColor();
        
            // make Initial Step
            for (int i = 0; i < 4; i++)
            {
                MakeNewStep();
            }
        }
    
    
        void InitColor()
        {
            // get random value and set background color 
            _hueValue = Random.Range(0, 10) / 10.0f;
            Camera.main.backgroundColor = Color.HSVToRGB(_hueValue, 0.6f, 0.8f);
        }

    
        public void MakeNewStep()
        {
            // ------------------------------------------------------------------------- make a new step
            int randomPosX;
            // If it's the first step, set position x to zero.
            if (_stepIndex == 1)
            {
                randomPosX = 0;
            }
            // it's NOT the first step, set position x to random value.
            else
            {
                randomPosX = Random.Range(-4, 5);
            }

            Vector2 newPosition = new Vector2(randomPosX, _stepIndex * _distanceToNextStep);
            GameObject newStep = Instantiate(stepPrefab, newPosition, Quaternion.identity);
            newStep.transform.SetParent(transform);
            newStep.transform.localScale = new Vector2(stepWidth, newStep.transform.localScale.y);

        
            // ------------------------------------------------------------------------- set color of the new step
            IncreaseHueValue(0.05f);
            newStep.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(_hueValue, 0.7f, 0.85f);
        
        
            // ------------------------------------------------------------------------- decrease the step width
            ChangeStepWidth(stepWidthDecrease);
        

            // ------------------------------------------------------------------------- increase the step index
            _stepIndex++;
        }

    
        private void IncreaseHueValue(float value)
        {
            _hueValue += value;
            if (_hueValue >= 1)
            {
                _hueValue -= 1;
            }
        }

    
        private void ChangeStepWidth(float value)
        {
            stepWidth += value;
            if (stepWidth <= stepMin)
            {
                stepWidth = stepMin;
            }
        }
    
    }
}
