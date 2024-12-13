using DG.Tweening;
using UnityEngine;

namespace _2D_BUNDLE.WallToWall2022.Scripts {
    public class BallEffectHandler : MonoBehaviour {

        private SpriteRenderer spriteRenderer;

        private float mainBall_originalScale;
        private Color mainBall_originalColor;

        [SerializeField] private GameObject outlineBall;
        private float outlineBall_originalScale;
        private Color outlineBall_originalColor;
    
        [SerializeField] private GameObject pfJumpEffect;
        [SerializeField] private GameObject pfWallTouchedEffect;
        [SerializeField] private GameObject pfObstacleTouchedEffect;
        [SerializeField] private GameObject pfExplodeEffect;

    


        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            mainBall_originalScale = transform.localScale.x;
            mainBall_originalColor = spriteRenderer.color;
            outlineBall_originalScale = outlineBall.transform.localScale.x;
            outlineBall_originalColor = outlineBall.GetComponent<SpriteRenderer>().color;
        
            transform.localScale = Vector3.zero;
            outlineBall.transform.localScale = Vector3.zero;
        }

    
        private void Start()
        {
            BallActionManager.Instance.onBallGenerated += OnBallGenerated;
            BallActionManager.Instance.onBallJumped += OnBallJumped;
            BallActionManager.Instance.onWallTouched += OnWallTouched;
            BallActionManager.Instance.onObstacleTouched += OnObstacleTouched;
            BallActionManager.Instance.onBallExplode += OnBallExplode;
        }



        private void OnBallGenerated()
        {
            Sequence ballGeneratedSequence = DOTween.Sequence();

            ballGeneratedSequence
                .Append(transform
                    .DOScale(mainBall_originalScale, .5f)
                    .SetEase(Ease.OutBounce)
                )
                .Append(
                    outlineBall.transform
                        .DOScale(outlineBall_originalScale, .3f)
                        .SetEase(Ease.OutBack)
                );
            
            ballGeneratedSequence.AppendInterval(.3f);
            
            ballGeneratedSequence
                .Append(
                    spriteRenderer
                        .DOColor(Color.white, .2f)
                        .SetEase(Ease.Flash, 2)
                )
                .Join(
                    outlineBall.GetComponent<SpriteRenderer>().
                        DOColor(Color.white, .2f)
                        .SetEase(Ease.Flash, 2)
                );

            ballGeneratedSequence.OnComplete(() =>
            {
                Ball.Instance.GameStart();
            });
        
        }
    
        private void OnBallJumped()
        {
            Instantiate(pfJumpEffect, transform.position, Quaternion.identity);
        }
    
    
        private void OnWallTouched()
        {
            Wall touchedWall = Ball.Instance.GetTouchedWall();
            Vector3 touchedPosition = Ball.Instance.GetTouchedPosition();

            WallTypeHolder.WallType touchedWallType = touchedWall.GetComponent<WallTypeHolder>().wallType;
        
        
            GameObject effect =  Instantiate(pfWallTouchedEffect, touchedPosition, Quaternion.identity);

            if (touchedWallType == WallTypeHolder.WallType.LEFT)
            {
                effect.transform.localRotation = Quaternion.Euler(0,0,-90);
            }
        
            if (touchedWallType == WallTypeHolder.WallType.RIGHT)
            {
                effect.transform.localRotation = Quaternion.Euler(0,0,90);
            }

        }
    
        private void OnObstacleTouched()
        {
            Vector2 position = Ball.Instance.GetTouchedPosition();
            Instantiate(pfObstacleTouchedEffect, position, Quaternion.identity);

            Sequence obstacleTouchedSequence = DOTween.Sequence();
            obstacleTouchedSequence.SetLink(gameObject);
            obstacleTouchedSequence.OnStart(() =>
            {
                spriteRenderer.color = mainBall_originalColor;
                outlineBall.GetComponent<SpriteRenderer>().color = outlineBall_originalColor;
            });

            obstacleTouchedSequence.Append(spriteRenderer.DOColor(Color.red, 1f).SetEase(Ease.Flash, 8));

            obstacleTouchedSequence.OnComplete(() =>
            {
                Ball.Instance.Explode();
            });

        }
    
        private void OnBallExplode()
        {
            spriteRenderer.enabled = false;
            outlineBall.GetComponent<SpriteRenderer>().enabled = false;
        
            Instantiate(pfExplodeEffect, transform.position, Quaternion.identity);
        
            Sequence sq = DOTween.Sequence();
            sq.AppendInterval(1f);
            sq.OnComplete(() =>
            {
                Ball.Instance.GameOvered();
            });


        }


    
    }
}


