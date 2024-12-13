using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _2D_BUNDLE.WallToWall2022.Scripts {
    public class WallToWallGameManager : MonoBehaviour
    {
        /// <summary>
        /// ���⵵ �̱����̳�? �Ʊ�� �� �ƴϾ��µ�...�ϴ� ����
        /// </summary>
        public static WallToWallGameManager Instance { get; private set; }

        [SerializeField] GameObject m_uiObj;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            Time.timeScale = 1f;
        }

        public void GameOvered()
        {
            m_uiObj.SetActive(false);
            GManager.Instance.IsGameOverFlag = true;
            Time.timeScale = 0f;
            ScoreManager.Instance.PlusCurScore();
        }
    }
}
