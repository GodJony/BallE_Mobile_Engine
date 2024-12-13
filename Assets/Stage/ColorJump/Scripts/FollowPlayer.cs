using UnityEngine;

namespace _2D_BUNDLE.ColorJump.Scripts {
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject playerObj;
        [SerializeField] private float smoothTime = 0.3F; 
        [SerializeField] private int yOffset;

        private Vector3 velocity = Vector3.zero;
    
    
        void LateUpdate()
        {
            Vector3 targetPosition = playerObj.transform.TransformPoint(new Vector3(0, yOffset, -10));

            // if Y of targetPosition is bigger than this transform (mainCamera), 
            // move this position
            if (targetPosition.y > transform.position.y)
            {
                targetPosition = new Vector3(0, targetPosition.y, targetPosition.z);
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);            
            }


        }

    }
}