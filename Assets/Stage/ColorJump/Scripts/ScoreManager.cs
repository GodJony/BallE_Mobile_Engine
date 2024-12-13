using TMPro;
using UnityEngine;

namespace _2D_BUNDLE.ColorJump.Scripts
{
    public class ScoreManager : MyScoreManager
    {
        public static ScoreManager Instance;

        // text
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI bestScoreText;

        // score
        private int currentScore;
        private int bestScore;


        private void Awake()
        {
            Instance = this;

            // get and set best score from PlayerPrefs
            bestScore = PlayerPrefs.GetInt("Stage_ColorJump_Best_Score", 0);
            bestScoreText.text = bestScore.ToString();

            // set current score to zero 
            currentScore = 0;
            scoreText.text = currentScore.ToString();
        }


        public void AddScore(int score)
        {
            currentScore += score;
            scoreText.text = currentScore.ToString();
            IsScore = currentScore;

            // compare and set best score
            if (currentScore > PlayerPrefs.GetInt("Stage_ColorJump_Best_Score", 0))
            {
                bestScoreText.text = currentScore.ToString();
                PlayerPrefs.SetInt("Stage_ColorJump_Best_Score", currentScore);
            }
        }

        public void AddCurScoore()
        {
            GManager.Instance.AddScoreInDic(m_SceneType,IsScore);
        }
    }
}
