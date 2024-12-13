using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _2D_BUNDLE.JumpAndShoot.Scripts {
    public class GameOverEffectPanel : MonoBehaviour
    {
    
        // GameOverEffectPanel object is activated by UIManager -> GameOver()
        private void Awake()
        {
            GetComponent<Image>()
                .DOColor(new Color(1,1,1,0), .5f) 
                .SetUpdate(true);  // To ignore the "Time.timeScale=0.1f" set in UIManager
        }
    }
}
