using _2D_BUNDLE.JumpAndShoot.Scripts;
using System.Collections;
using TMPro;
using UnityEngine;


namespace _2D_BUNDLE.JumpAndShoot.Scripts
{
    public class UIManager : MonoBehaviour
    {
        /// <summary>
        /// Jump And Shoot UI Manager
        /// </summary>
        public static UIManager Instance;

        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI bestValueText;
        public TextMeshProUGUI bestText;

        /// <summary>
        /// Á¡ÇÁ ¾Ø ½¸ Äµ¹ö½º
        /// </summary>
        [SerializeField] private GameObject m_JumpShootCanvas = null;

        private void Awake()
        {
            Instance = this;
        }


        public void GameOver()
        {
            StartCoroutine(GameOverCoroutine());
        }


        IEnumerator GameOverCoroutine()
        {
            GManager.Instance.IsGameOverFlag = true;
            m_JumpShootCanvas.SetActive(false);

            Time.timeScale = 0.1f;
            scoreText.color = Color.white;
            bestText.color = Color.gray;
            bestValueText.color = Color.gray;

            yield return new WaitForSecondsRealtime(0.5f);

            Time.timeScale = 0.02f;

            yield return new WaitForSecondsRealtime(2.5f);

            ScoreManager.Instance.AddCurScore();

            
            while(!GManager.Instance.IsPrefSaveFlag) yield return null;

            Time.timeScale = 0f;
        }

        
    }
}