using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

// using TMPro;

namespace _2D_BUNDLE.Wave.Scripts
{
    public class WaveGameManager : MonoBehaviour
    {
        [SerializeField] private GameObject touchToMoveTextObj;
        [SerializeField] private GameObject StartFadeInObj;

        /// <summary>
        /// Wave 스테이지의 캔버스 게임 오브젝트
        /// </summary>
        [SerializeField] private GameObject m_waveCanvas;


        void Awake()
        {
            Application.targetFrameRate = 60;

            Time.timeScale = 1.0f;

            StartCoroutine(FadeIn());
        }

        void Update()
        {
            if (touchToMoveTextObj.activeSelf == false) return;
            if (Input.GetMouseButton(0))
            {
                touchToMoveTextObj.SetActive(false);
            }
        }

        IEnumerator FadeIn()
        {
            StartFadeInObj.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            StartFadeInObj.SetActive(false);
        }

        public void GameOver()
        {
            StartCoroutine(GameOverCoroutine());
        }

        private IEnumerator GameOverCoroutine()
        {
            ScoreManager.Instance.PlusCurScore();
            m_waveCanvas.SetActive(false);
            GManager.Instance.IsGameOverFlag = true;
            Time.timeScale = 0.1f;
            yield return new WaitForSecondsRealtime(0.5f);

            while (!GManager.Instance.IsPrefSaveFlag)
            {
                yield return null;
            }
        }

        

        /// <summary>
        /// 다른 사람이 만든 리스타트 잠시 삭제 ㄴㄴ
        /// </summary>
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
