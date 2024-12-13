using UnityEngine;

namespace _2D_BUNDLE.ColorJump.Scripts {
    public class Step : MonoBehaviour
    {

        private Vector2 startPosition;
        private Vector2 targetPosition;
        private Vector3 velocity = Vector3.zero;

        private bool isArrived = false;
    
        [SerializeField] private float smoothTime;

    
        void Start()
        {
            // set targetPosition to current position
            targetPosition = transform.position;

            // set startPosition to left or right from targetPosition
            float distance = 10f;
            if (Random.Range(0, 2) == 0)
            {
                startPosition = new Vector2(targetPosition.x - distance, targetPosition.y);
            }
            else
            {
                startPosition = new Vector2(targetPosition.x + distance, targetPosition.y);
            }
        
            // set current position to start position
            transform.position = startPosition;
        }

        void Update()
        {
            if (isArrived == false)
            {
                if (Vector2.Distance(targetPosition, transform.position) > 0.01f)
                {
                    MoveToTargetPosition();
                }
                else
                {
                    isArrived = true;
                }
            }
        }

    
        void MoveToTargetPosition()
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        }
    }
}
