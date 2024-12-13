using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _2D_BUNDLE.JumpAndShoot.Scripts {
    public class StartEffectPanel : MonoBehaviour
    {
    
        // StartEffectPanel object is activated by GameManager.
        private void Awake()
        {
            GetComponent<Image>().DOFade(0, .5f);
        }
    }
}
