using UnityEngine;

namespace _2D_BUNDLE.WallToWall2022.Scripts {
    public class Ball : MonoBehaviour {
    
        /// <summary>
        /// 여기는 상태 패턴이네..
        /// </summary>
        public static Ball Instance { get; private set; }

        private enum State {
            WAITING_TO_START, 
            PLAYING, 
            OBSTACLE_TOUCHED, 
            EXPLODE,
            GAME_OVERED
        }

        private State state;
    

        private Rigidbody2D rb2D;

        private Wall touchedWall;
        private Vector3 touchedPosition;
    
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        
            rb2D = GetComponent<Rigidbody2D>();

            SetState(State.WAITING_TO_START);

        }

        private void Start()
        {
            BallActionManager.Instance.InvokeOnBallGenerated();
        }

        public void GameStart()
        {
            SetState(State.PLAYING);
            BallActionManager.Instance.InvokeOnGameStarted();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && state == State.PLAYING)
            {
                Jump();
                BallActionManager.Instance.InvokeOnBallJumped();
            }
        }

        private void Jump()
        {
            if (rb2D.velocity.x >= 0)
            {
                rb2D.velocity = new Vector2(2, 5);
            }
            else
            {
                rb2D.velocity = new Vector2(-2, 5);
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out WallTypeHolder wallTypeHolder))
            {
                if (wallTypeHolder.wallType == WallTypeHolder.WallType.LEFT || wallTypeHolder.wallType == WallTypeHolder.WallType.RIGHT)
                {
                    touchedPosition = col.contacts[0].point;
                    touchedWall = col.gameObject.GetComponent<Wall>();
                    BallActionManager.Instance.InvokeOnWallTouched();
                }
            }

            if (col.gameObject.TryGetComponent(out Obstacle obstacle))
            {
                touchedPosition = col.contacts[0].point;
                SetState(State.OBSTACLE_TOUCHED);
                BallActionManager.Instance.InvokeOnObstacleTouched();
            }
        
        }

        public Wall GetTouchedWall()
        {
            return touchedWall;
        }

        public Vector3 GetTouchedPosition()
        {
            return touchedPosition;
        }
    
    
        public void Explode()
        {
            SetState(State.EXPLODE);
            BallActionManager.Instance.InvokeOnBallExploded();
        }

        public void GameOvered()
        {
            SetState(State.GAME_OVERED);
            WallToWallGameManager.Instance.GameOvered();
        }
        
        /// <summary>
        /// 리스타트
        /// </summary>
        public void ReStart()
        {
            SetState(State.WAITING_TO_START);
            Debug.Log("재실행");
        }

        private void SetState(State state)
        {
            this.state = state;
            if (this.state == State.PLAYING)
            {
                GetComponent<CircleCollider2D>().enabled = true;
                rb2D.isKinematic = false;
            }
            else
            {
                GetComponent<CircleCollider2D>().enabled = false;
                rb2D.isKinematic = true;
                rb2D.velocity = Vector2.zero;
            }
        }
    }
}