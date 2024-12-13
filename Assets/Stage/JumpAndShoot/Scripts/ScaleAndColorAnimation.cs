using DG.Tweening;
using UnityEngine;

namespace _2D_BUNDLE.JumpAndShoot.Scripts {
    public class ScaleAndColorAnimation : MonoBehaviour
    {
        [Header("Scale Animation")]
        [SerializeField] private float scale_duration;
        [SerializeField] private float scale_endValue;
        [SerializeField] private Ease scale_ease;
    
        [Header("Color Animation")]
        [SerializeField] private float color_duration;
        [SerializeField] private Color color_endValue;
        [SerializeField] private Ease color_ease;

        private Sequence scaleSequence;
        private Sequence colorSequence;

        private bool isScalePlaying = false;
        private bool isColorPlaying = false;
    

        private void Start()
        {
            scaleSequence = DOTween.Sequence();
            colorSequence = DOTween.Sequence();
        
            // if duration is not 0, start animation
            if (scale_duration != 0)
            {
                ScaleAnimation();
            }

            // if duration is not 0, start animation
            if (color_duration != 0)
            {
                ColorAnimation();
            }

        }
    
    
        private void ScaleAnimation()
        {
            isScalePlaying = true;
            scaleSequence
                .Join(transform.DOScale(scale_endValue, scale_duration))
                .SetEase(scale_ease)
                .SetLink(gameObject)
                .OnComplete(OnScaleCompleted);
        }
    
    
        private void ColorAnimation()
        {
            isColorPlaying = true;
            colorSequence
                .Join(GetComponent<SpriteRenderer>().DOColor(color_endValue, color_duration))
                .SetEase(color_ease)
                .SetLink(gameObject)
                .OnComplete(OnColorCompleted);
        }

    
        private void OnScaleCompleted()
        {
            isScalePlaying = false;
            // if all animation is ended, destroy this game object
            if (IsAllEnded())
            {
                Destroy(gameObject);
            }
        }
    
    
        private void OnColorCompleted()
        {
            isColorPlaying = false;
            // if all animation is ended, destroy this game object
            if (IsAllEnded())
            {
                Destroy(gameObject);
            }
        }
    
    
        private bool IsAllEnded()
        {
            return (!isScalePlaying && !isColorPlaying);
        }
    }
}
