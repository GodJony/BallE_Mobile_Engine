using System;
using UnityEngine;

namespace _2D_BUNDLE.WallToWall2022.Scripts {
    public class ScoreManager : MyScoreManager
    {
        /// <summary>
        /// 이건 또 왜 싱글톤으로 되어있는건지
        /// 알 수가 없다
        /// 이 구조를 바꾸기엔 시간이 부족하니, 그냥 하자
        /// </summary>
        public static ScoreManager Instance { get; private set; }

        /// <summary>
        /// 이번엔 델리게이트네 흐...
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
        /// 라운드 종료 시 현재 점수를 더함
        /// </summary>
        public void PlusCurScore()
        {
            GManager.Instance.AddScoreInDic(m_SceneType, IsScore);
            IsScore = 0;
        }
    }
}
