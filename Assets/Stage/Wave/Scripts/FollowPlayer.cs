using UnityEngine;

namespace _2D_BUNDLE.Wave.Scripts {
    public class FollowPlayer : MonoBehaviour
    {

        [SerializeField] private GameObject player;
        [SerializeField] private float smoothTime = 0.3F;
        [SerializeField] private int yOffset;
    
        private Vector3 velocity;

    
        void Update()
        {
            Vector3 targetPosition = player.transform.TransformPoint(new Vector3(0, yOffset, -10));
            targetPosition = new Vector3(0, targetPosition.y, targetPosition.z);

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }

    }
}