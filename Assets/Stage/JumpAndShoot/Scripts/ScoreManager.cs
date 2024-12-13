using TMPro;
using UnityEngine;

namespace _2D_BUNDLE.JumpAndShoot.Scripts {
    public class ScoreManager : MyScoreManager {

        /// <summary>
        /// Jump And Shoot ScoreManager
        /// </summary>
        public static ScoreManager Instance;

        [SerializeField] private TextMeshProUGUI currentScoreText;
        [SerializeField] private TextMeshProUGUI bestScoreText;
    
        private int _score = 0;

    
        private void Awake()
        {
            Instance = this;
        }
    

        public void AddScore(int value)
        {
            // add score & update score text
            _score += value;
            currentScoreText.text = _score.ToString();

            IsScore = _score;

            // update best score
            if (_score > PlayerPrefs.GetInt("Stage_JumpAndShoot_Best_Score", 0))
            {
                bestScoreText.text = _score.ToString();
                PlayerPrefs.SetInt("Stage_JumpAndShoot_Best_Score", _score);
            }
        }

        /// <summary>
        /// 스코어 저장
        /// </summary>
        public void AddCurScore()
        {
            GManager.Instance.AddScoreInDic(m_SceneType, IsScore);
            IsScore = 0;
        }
    }
}
