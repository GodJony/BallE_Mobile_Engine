using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _2D_BUNDLE.WallToWall2022.Scripts {
    public class Obstacle : MonoBehaviour {

        private SpriteRenderer spriteRenderer;
        private PolygonCollider2D polygonCollider2D;

        [SerializeField] private GameObject obstacleWhite;
        [SerializeField] private GameObject pfObstacleExplode;

    
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            polygonCollider2D = GetComponent<PolygonCollider2D>();
        }
    

        private void Start()
        {
            polygonCollider2D.enabled = false;
            InitialEffect();
        }

    
        private void InitialEffect()
        {
            Color initialColor = spriteRenderer.color;
            spriteRenderer.color = Color.red;

            Sequence initialSequence = DOTween.Sequence();
        
            initialSequence.SetLink(gameObject);
        
            initialSequence.Append(
                transform
                    .DOLocalMoveX(-0.5f, 0.5f)
                    .From()
                    .SetEase(Ease.OutBounce));
        
            initialSequence.Join(
                spriteRenderer
                    .DOColor(initialColor, 1f));

            initialSequence.OnComplete(() =>
            {
                polygonCollider2D.enabled = true;
            });


            Sequence whiteObstacleSequence = DOTween.Sequence();
            whiteObstacleSequence.SetLink(obstacleWhite);
            whiteObstacleSequence.Append(
                obstacleWhite.transform
                    .DOLocalMoveX(-0.5f, 0.7f)
                    .From()
                    .SetEase(Ease.OutFlash)
            );
            whiteObstacleSequence.Join(
                obstacleWhite.GetComponent<SpriteRenderer>()
                    .DOColor(new Color(1,1,1,0), 0.7f));
        
            whiteObstacleSequence.OnComplete(() =>
            {
                Destroy(obstacleWhite);
            });
        

        }

        public void DestroyEffect()
        {
            polygonCollider2D.enabled = false;
            spriteRenderer.color = new Color(0, 0, 0, .3f);

            float randomX = Random.Range(.5f, 1.5f);
            float randomLifeTime = Random.Range(.5f, 1f);
            float randomAngle = Random.Range(-30, 30);

            Sequence destroySequence = DOTween.Sequence();
            destroySequence.SetLink(gameObject);

            destroySequence.Append(transform.DOLocalMoveX(randomX, randomLifeTime).SetEase(Ease.OutQuad));
            destroySequence.Join(transform.DOScale(.1f, randomLifeTime).SetEase(Ease.OutQuad));
            destroySequence.Join(transform.DOLocalRotate(new Vector3(0, 0, randomAngle), randomLifeTime).SetEase(Ease.OutQuad));

            destroySequence.OnComplete(() =>
            {
                Instantiate(pfObstacleExplode, transform.position, Quaternion.Euler(transform.rotation.eulerAngles));
                Destroy(gameObject);
            });

        }

    }
}
