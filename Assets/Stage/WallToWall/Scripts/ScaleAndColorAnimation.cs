using DG.Tweening;
using UnityEngine;

namespace _2D_BUNDLE.WallToWall2022.Scripts {
    public class ScaleAndColorAnimation : MonoBehaviour {
    
        [Header("Scale")]
        [SerializeField] private float scale_duration;
        [SerializeField] private float scale_endValue;
        [SerializeField] private Ease scale_ease;
    
        [Header("Color")]
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

            if (scale_duration != 0)
            {
                ScaleAnimation();
            }

            if (color_duration != 0)
            {
                ColorAnimation();
            }
        }

    
        private void ScaleAnimation()
        {
            isScalePlaying = true;

            scaleSequence
                .Append(transform.DOScale(scale_endValue, scale_duration))
                .SetEase(scale_ease)
                .SetLink(gameObject)
                .OnComplete(OnScaleComplete);
        }

        private void OnScaleComplete()
        {
            isScalePlaying = false;
            if (!isColorPlaying)
            {
                Destroy(gameObject);
            }
        }
    
    
        private void ColorAnimation()
        {
            isColorPlaying = true;

            colorSequence
                .Append(GetComponent<SpriteRenderer>().DOColor(color_endValue, color_duration))
                .SetEase(color_ease)
                .SetLink(gameObject)
                .OnComplete(OnColorCompleted);
        }

    
        private void OnColorCompleted()
        {
            isColorPlaying = false;
            if (!isScalePlaying)
            {
                Destroy(gameObject);
            }
        }



    }
}
