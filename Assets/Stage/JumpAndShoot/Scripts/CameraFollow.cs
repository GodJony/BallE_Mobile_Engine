using UnityEngine;

namespace _2D_BUNDLE.JumpAndShoot.Scripts {
    public class CameraFollow : MonoBehaviour
    {

        [SerializeField] private GameObject player;
        [SerializeField] private float smoothTime = 0.3F;
        [SerializeField] private int yOffset;
    
        private Vector3 _velocity = Vector3.zero;
    
    
        void LateUpdate()
        {
            Follow();
        }

    
        private void Follow()
        {
            Vector3 targetPosition = player.transform.TransformPoint(new Vector3(0, 0, -10));
            targetPosition = new Vector2(targetPosition.x, targetPosition.y + yOffset);

            if (targetPosition.y > transform.position.y)
            {
                targetPosition = new Vector3(0, targetPosition.y, -10);
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);    
            }
        }

    }
}