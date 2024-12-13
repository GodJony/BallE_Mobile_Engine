using UnityEngine;
using UnityEngine.UI;

namespace _2D_BUNDLE.Wave.Scripts
{
    public class ScoreManager : MyScoreManager
    {
        public static ScoreManager Instance { get; private set; }

        [SerializeField] private Text m_currentScoreText;
        [SerializeField] private Text m_bestScoreText;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        
            else
            {
                Destroy(gameObject);
            }

            m_currentScoreText.text = "0";
            m_bestScoreText.text = PlayerPrefs.GetInt("Stage_Wave_Best_Score", 0).ToString();
        }


        public void AddScore()
        {
            IsScore++;
            m_currentScoreText.text = IsScore.ToString();

            if (IsScore > PlayerPrefs.GetInt("Stage_Wave_Best_Score", 0))
            {
                PlayerPrefs.SetInt("Stage_Wave_Best_Score", IsScore);
                m_bestScoreText.text = PlayerPrefs.GetInt("Stage_Wave_Best_Score", 0).ToString();
            }
        }

        public int GetScore()
        {
            return IsScore;
        }

        /// <summary>
        /// 라운드 종료 시 현재 점수를 더함
        /// </summary>
        public void PlusCurScore()
        {
            GManager.Instance.AddScoreInDic(m_SceneType, IsScore);
            IsScore = 0;
        }
    }
}
