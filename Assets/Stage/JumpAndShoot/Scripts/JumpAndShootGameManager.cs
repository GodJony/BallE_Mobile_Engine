using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _2D_BUNDLE.JumpAndShoot.Scripts {
    public class JumpAndShootGameManager : MonoBehaviour
    {
        public static JumpAndShootGameManager Instance;
    
        public GameObject StartEffectPanel;
    

        void Awake()
        {
            Instance = this;

            Application.targetFrameRate = 60;
            Time.timeScale = 1.0f;
        }

    
        void Start()
        {
            StartCoroutine(StartEffect());
        }


        IEnumerator StartEffect()
        {
            StartEffectPanel.SetActive(true);
            yield return new WaitForSecondsRealtime(0.5f);
            StartEffectPanel.SetActive(false);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
