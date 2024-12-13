using DG.Tweening;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    [SerializeField] private GameObject pfBall;
    [SerializeField] private GameObject ballPosition;

    [SerializeField] private GameObject circle;

    /// <summary>
    /// 공 스프라이트
    /// </summary>
    private Sprite m_ballSprite;

    private void Start()
    {
        GenerateBallEffect();

        circle.transform.position = ballPosition.transform.position;
        circle.transform.localScale = Vector2.zero;
    }

    /// <summary>
    /// 스프라이트 세팅
    /// </summary>
    /// <param name="sprite">스킨</param>
    public void SetBallSprite(Sprite argSprite)
    {
        m_ballSprite = argSprite;
    }

    private void GenerateBallEffect()
    {
        Sequence generateBallSequence = DOTween.Sequence();

        generateBallSequence
            .Append(circle.transform
                .DOScale(.9f, .5f)
                .SetEase(Ease.OutCirc)
            );

        generateBallSequence
            .AppendCallback(GenerateBall);

        generateBallSequence.AppendInterval(2f);

        generateBallSequence
            .Append(circle.transform
                .DOScale(0, 0.5f)
                .SetEase(Ease.OutCirc)
            )
            .Join(circle.GetComponent<SpriteRenderer>()
                .DOFade(0, .5f)
            );
    }

    private void GenerateBall()
    {
        GameObject _ball = Instantiate(pfBall, ballPosition.transform.position, Quaternion.identity);

        SpriteRenderer ballSpriteRenderer = _ball.GetComponent<SpriteRenderer>();
        if (ballSpriteRenderer != null && m_ballSprite != null)
        {
            ballSpriteRenderer.sprite = m_ballSprite;
        }
    }
}
