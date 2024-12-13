using TMPro;
using UnityEngine;

namespace _2D_BUNDLE.LeftOrRight.Scripts {
    public class ScoreManager : MonoBehaviour {
    
        public static ScoreManager Instance;
    
        [SerializeField] private TextMeshProUGUI currentScoreText;
        [SerializeField] private TextMeshProUGUI BestScoreText;
        [SerializeField] private TextMeshProUGUI BestText;

        private int currentScore;
    
    
        private void Awake()
        {
            if (Instance != null) 
                Destroy(this);
            else 
                Instance = this;
        }


        private void Start()
        {
            BestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
            currentScore = 0;
            currentScoreText.text = currentScore.ToString();
        
            ActionManager.onPlayerMoved += OnPlayerMoved;
            ActionManager.onGameEnded += OnGameEnded;
        }
    

        private void OnDestroy()
        {
            ActionManager.onPlayerMoved -= OnPlayerMoved;
            ActionManager.onGameEnded -= OnGameEnded;
        }

    
        private void OnPlayerMoved()
        {
            AddScore(1);
        }


        private void AddScore(int score)
        {
            currentScore += score;
            currentScoreText.text = currentScore.ToString();

            if (currentScore > PlayerPrefs.GetInt("BestScore", 0))
            {
                BestScoreText.text = currentScore.ToString();
                PlayerPrefs.SetInt("BestScore", currentScore);
            }
        }


        private void GameOvered()
        {
            currentScoreText.color = Color.white;
            BestScoreText.color = Color.white;
            BestText.color = Color.white;
        }


        private void OnGameEnded()
        {
            GameOvered();
        
        }

        public int GetCurrentScore()
        {
            return currentScore;
        }

    }
}
