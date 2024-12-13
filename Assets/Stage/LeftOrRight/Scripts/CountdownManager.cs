using TMPro;
using UnityEngine;

namespace _2D_BUNDLE.LeftOrRight.Scripts {
    public class CountdownManager : MonoBehaviour
    {
    
        [SerializeField] private TextMeshPro text;

        private float countdownTime = 9.9f;


        private void Start()
        {
            ActionManager.onPlayerGetItem += OnPlayerGetItem;
            ActionManager.onPlayerExploded += OnPlayerExploded;
        }
    

        private void OnDestroy()
        {
            ActionManager.onPlayerGetItem -= OnPlayerGetItem;
            ActionManager.onPlayerExploded -= OnPlayerExploded;
        }
    


        private void Update()
        {
            // if game is playing, start countdown
            if (Player.Instance.IsStarted() && !Player.Instance.IsDead())
            {
                CountDown();
            }
        }


        private void CountDown()
        {
            countdownTime -= Time.deltaTime;
            text.text = countdownTime.ToString("F1");
        
            if (countdownTime <= 0)
            {
                countdownTime = 0f;
                ActionManager.onCountdownEnded?.Invoke();
            }
        }

    
        public float GetTime()
        {
            return countdownTime;
        }

    
        private void OnPlayerGetItem(int time)
        {
            countdownTime += time;
        }
    
    
        private void OnPlayerExploded()
        {
            text.enabled = false;
        }
    }
}
