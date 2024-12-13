using System;
using UnityEngine;

namespace _2D_BUNDLE.WallToWall2022.Scripts {
    public class ScoreManager : MyScoreManager
    {
        /// <summary>
        /// �̰� �� �� �̱������� �Ǿ��ִ°���
        /// �� ���� ����
        /// �� ������ �ٲٱ⿣ �ð��� �����ϴ�, �׳� ����
        /// </summary>
        public static ScoreManager Instance { get; private set; }

        /// <summary>
        /// �̹��� ��������Ʈ�� ��...
        /// </summary>
        public Action onCurrentScoreChanged;
        public Action onBestScoreChanged;

        private int currentScore;
        private int bestScore;
    
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            currentScore = 0;
            bestScore = PlayerPrefs.GetInt("Stage_WallToWall_Best_Score");

        }

        private void Start()
        {
            BallActionManager.Instance.onWallTouched += OnWallTouched;
        }

        private void OnWallTouched()
        {
            UpdateScore(1);
        }

        private void UpdateScore(int argScore)
        {
            currentScore += argScore;
            IsScore = currentScore;
            onCurrentScoreChanged?.Invoke();
            if (currentScore > bestScore)
            {
                bestScore = currentScore;
                onBestScoreChanged?.Invoke();
                PlayerPrefs.SetInt("Stage_WallToWall_Best_Score", bestScore);
            }
        }

        public int GetCurrentScore()
        {
            return currentScore;
        }

        public int GetBestScore()
        {
            return bestScore;
        }

        /// <summary>
        /// ���� ���� �� ���� ������ ����
        /// </summary>
        public void PlusCurScore()
        {
            GManager.Instance.AddScoreInDic(m_SceneType, IsScore);
            IsScore = 0;
        }
    }
}
