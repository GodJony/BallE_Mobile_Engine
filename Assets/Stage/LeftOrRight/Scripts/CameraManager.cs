using System.Collections;
using UnityEngine;

namespace _2D_BUNDLE.LeftOrRight.Scripts {
    public class CameraManager : MonoBehaviour
    {

        [SerializeField] private GameObject player;
    
    
        [SerializeField] private int yOffset = -3;
        [SerializeField] private float followTime = 0.3F;

        // values for camera zoom in
        private int startOrthographicSize = 25;
        private int endOrthographicSize = 13;
    
        // values for SmoothDamp when game start
        private float startSmoothTime = 0.5F;
        private float startVelocity;

        // values for SmoothDamp when player dead
        private float deadSmoothTime = 0.1F;
        private float deadVelocity ;

        private Vector3 followVelocity = Vector3.zero;

    

        void Start()
        {
            // set camera orthographicSize  
            Camera.main.orthographicSize = startOrthographicSize;

            // Zoom in when the game starts
            StartCoroutine(ZoomIn());
     
            // assign a method to the action
            ActionManager.onPlayerOut += OnPlayerOut;
        }

        private void OnDestroy()
        {
            ActionManager.onPlayerOut -= OnPlayerOut;
        }


        private void Update()
        {
            FollowThePlayer();
        }
    

        private IEnumerator ZoomIn()
        {
            yield return new WaitForSecondsRealtime(0.5f);

            while (Camera.main.orthographicSize > endOrthographicSize + 0.1f)
            {
                Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, endOrthographicSize, ref startVelocity, startSmoothTime);
                yield return 0;
            }
        }

    
        private void FollowThePlayer()
        {
            Vector3 targetPosition = player.transform.TransformPoint(new Vector3(0, yOffset, -10));
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref followVelocity, followTime);
        }


        private void OnPlayerOut()
        {
            yOffset = 0;
        
            StartCoroutine(GameOverZoomIn());
        }

    
        private IEnumerator GameOverZoomIn()
        {
            while (Camera.main.orthographicSize > 10)
            {
                Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, 10, ref deadVelocity, deadSmoothTime);
                yield return 0;
            }
        }


    }
}
