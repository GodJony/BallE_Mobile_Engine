using UnityEngine;
using UnityEngine.SceneManagement;

namespace _2D_BUNDLE.ColorJump.Scripts {
    public class ColorJumpGamePlayManager : MonoBehaviour {
    
        public static ColorJumpGamePlayManager Instance;

        private void Awake()
        {
            Instance = this;
        
            Application.targetFrameRate = 60;
            Time.timeScale = 1;
        }
    
    
        public void GameOver()
        {
            StartCoroutine(GetComponent<EffectManager>().GameOverEffectCoroutine());
        }

    
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
