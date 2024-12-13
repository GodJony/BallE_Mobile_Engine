using UnityEngine;

namespace _2D_BUNDLE.LeftOrRight.Scripts {
    public class RedMask : MonoBehaviour {

    
        [SerializeField] private CountdownManager countdownManager;
    
        [SerializeField] private GameObject redMask;
        
    
        private void Start()
        {
            ActionManager.onPlayerOut += OnPlayerOut;
            ActionManager.onCountdownEnded += OnCountdownEnded;
        }
    
    
        private void OnDestroy()
        {
            ActionManager.onPlayerOut -= OnPlayerOut;
            ActionManager.onCountdownEnded -= OnCountdownEnded;
        }
    

        private void Update()
        {
            // update redMask's scale by time value
            float time = Mathf.Clamp(countdownManager.GetTime(), 0f, 9.9f);
            transform.localScale = new Vector2(1, 1f - time / 9.9f);
        }

    
        private void OnPlayerOut()
        {
            redMask.SetActive(false);
        }
    
        private void OnCountdownEnded()
        {
            redMask.SetActive(false);
        }

    
    }
}
