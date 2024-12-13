using UnityEngine;
using Random = UnityEngine.Random;

namespace _2D_BUNDLE.JumpAndShoot.Scripts {
    public class StepManager : MyScoreManager {

        public static StepManager Instance;
    
        [SerializeField] private GameObject StepPrefab;

        [Space]
        // Initial width value
        [SerializeField] private float stepWidth;
        // Initial height value
        [SerializeField] private float stepHeight;
        [Space]
        // width reduction value (As player go up, the width decreases)
        [SerializeField] private  float decreaseStepWidth;
        // Minimum width value
        [SerializeField] private  float minimumStepWidth;
        [Space]
        // Number of initial steps
        [SerializeField] private  int NumberOfStartSteps = 4;
        [SerializeField] private  int DistanceToNextStep = 6;

        private int _stepIndex = 0;
    
        private float _halfWidth;


        private void Awake()
        {
            Instance = this;
        }

    
        void Start()
        {
            // save distance to right wall
            _halfWidth = GetDisplayBound.Instance.GetRightWallPosition();
        
            // Initial step generation
            InitSteps();
        }

    
        void InitSteps()
        {
            for (int i = 0; i < NumberOfStartSteps; i++)
            {
                CreateNewStep();
            }
        }


        public void CreateNewStep()
        {
            int randomPosX;
            if (_stepIndex == 0) 
            {
                randomPosX = 0;
            }
            else
            {
                int maxPosX = 5;
                int minPosX = -4;
                randomPosX = Random.Range(minPosX, maxPosX); // -4 ~ 4
            }

            // set position of new step
            Vector2 stepPosition = new Vector2(randomPosX, _stepIndex * DistanceToNextStep);
            // generate new step
            GameObject stepObj = Instantiate(StepPrefab, stepPosition, Quaternion.identity);
            // set as child of current object(StepManager)
            stepObj.transform.SetParent(transform);
            // set step width
            stepObj.transform.localScale = new Vector2(stepWidth, stepHeight);

    
        
            // set a moving distance of new step
            stepObj.GetComponent<Step>().SetDistance(Random.Range(1, _halfWidth));
            //set velocity of new step
            if (_stepIndex == 0)
            {
                stepObj.GetComponent<Step>().SetVelocity(0);
            }
            else
            {
                float minVelocity = 1.5f;
                float maxVelocity = 3.5f;
                stepObj.GetComponent<Step>().SetVelocity(Random.Range(minVelocity, maxVelocity));
            }

            // reduce the width of the step
            if (stepWidth > minimumStepWidth)
            {
                stepWidth -= decreaseStepWidth;
            }
   
            // increase step index
            _stepIndex++;


        }
    }
}
