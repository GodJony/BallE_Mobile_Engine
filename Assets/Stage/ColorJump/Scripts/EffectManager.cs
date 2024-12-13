using System.Collections;
using TMPro;
using UnityEngine;

namespace _2D_BUNDLE.ColorJump.Scripts {
    public class EffectManager : MonoBehaviour {
    
        public static EffectManager Instance;
    
        // ui panel
        [SerializeField] private GameObject startEffectPanel;
        [SerializeField] private GameObject deadEffectPanel;

        /// <summary>
        /// 컬러 점프 캔버스
        /// </summary>
        [SerializeField] private GameObject m_colorjumpCanvas= null;
    
        // text
        [SerializeField] private TextMeshProUGUI touchToStartGame;
        [SerializeField] private TextMeshProUGUI scoreText;



        private void Awake()
        {
            Instance = this;
        }
    

        private void Start()
        {
            StartCoroutine(WhiteFlashEffect());
        }
    
    
        // play flashing white effect when game start
        private IEnumerator WhiteFlashEffect()
        {
            startEffectPanel.SetActive(true);
            yield return new WaitForSecondsRealtime(0.5f);
            startEffectPanel.SetActive(false);
        }

    
        public void StartGame()
        {
            touchToStartGame.gameObject.SetActive(false);
        }


    
        public IEnumerator GameOverEffectCoroutine()
        {
            m_colorjumpCanvas.SetActive(false);
            ColorJumpGamePlayManager.Instance.GameOver();
            // set timeScale to move slowly.
            Time.timeScale = 0.1f;
            // play flashing red effect when game start
            deadEffectPanel.SetActive(true);
            scoreText.color = Color.white;
        
            yield return new WaitForSecondsRealtime(1.0f);

            ScoreManager.Instance.AddCurScoore();

            while (!GManager.Instance.IsPrefSaveFlag) yield return null;

            // set timeScale zero to do not move.
            Time.timeScale = 0.0f;
            deadEffectPanel.SetActive(true);

        }
    }
}
