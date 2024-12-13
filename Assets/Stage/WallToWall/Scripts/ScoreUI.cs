using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _2D_BUNDLE.WallToWall2022.Scripts {
    public class ScoreUI : MonoBehaviour {
    
        private TextMeshProUGUI currentScoreText;
        private TextMeshProUGUI bestScoreText;


        private void Awake()
        {
            currentScoreText = transform.Find("currentScoreText").GetComponent<TextMeshProUGUI>();
            bestScoreText = transform.Find("bestScoreText").GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            ScoreManager.Instance.onCurrentScoreChanged += OnCurrentScoreChanged;
            ScoreManager.Instance.onBestScoreChanged += OnBestScoreChanged;

            currentScoreText.text = ScoreManager.Instance.GetCurrentScore().ToString();
            bestScoreText.text = ScoreManager.Instance.GetBestScore().ToString();
        }

        private void OnCurrentScoreChanged()
        {
            currentScoreText.text = ScoreManager.Instance.GetCurrentScore().ToString();

            Sequence currentScoreSequence = DOTween.Sequence();
            currentScoreSequence.Append(currentScoreText.transform.DOScale(1.1f, .1f));
            currentScoreSequence.Append(currentScoreText.transform.DOScale(1.0f, .1f));
            currentScoreSequence.SetLink(gameObject);
        }
    
        private void OnBestScoreChanged()
        {
            bestScoreText.text = ScoreManager.Instance.GetBestScore().ToString();

            Color currentColor = bestScoreText.color;

            Sequence bestScoreSequence = DOTween.Sequence();
            bestScoreSequence.Append(bestScoreText.transform.DOScale(1.5f, .1f));
            bestScoreSequence.Join(bestScoreText.DOColor(Color.white, .1f));

            bestScoreSequence.Append(bestScoreText.DOColor(Color.black, .1f));
            bestScoreSequence.Append(bestScoreText.DOColor(Color.white, .1f));

            bestScoreSequence.Append(bestScoreText.transform.DOScale(1.0f, .2f));
            bestScoreSequence.Join(bestScoreText.DOColor(currentColor, .2f));
        
        }

  

    }
}
