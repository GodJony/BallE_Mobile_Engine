using System.Collections;
using TMPro;
using UnityEngine;

namespace _2D_BUNDLE.LeftOrRight.Scripts {
    public class Player : MonoBehaviour {
    
        public static Player Instance;
    
        // Movement
        private int moveDirection;
        private float moveDistanceX;
        private float moveDistanceY;
    
        // Effect
        [SerializeField] private GameObject fx_Dead;
    
        [SerializeField] private TextMeshPro timeLimitText;
    
        private bool isStairsReady = false;
        private bool isStarted = false;
        private bool isDead = false;

    
        private void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
                Instance = this;
        }


        private void Start()
        {
            // get distance between stair from StairsManager
            (moveDistanceX, moveDistanceY) = GameObject.Find("stairsManager").GetComponent<StairsManager>().GetDistance();

            ActionManager.onCountdownEnded += OnCountDownEnded;
        }

        private void OnDestroy()
        {
            ActionManager.onCountdownEnded -= OnCountDownEnded;
        }


        private void Update()
        {
            // get input if game is playing
            if (isDead || !isStairsReady) return;
            GetInput();
        }

    
        private void GetInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isStarted == false)
                {
                    // Game Start! 
                    isStarted = true;
                    ActionManager.onPlayerFirstMoved?.Invoke();
                }

                if (Input.mousePosition.x < Screen.width / 2f)
                {
                    // move to same direction
                    Move();
                }
                else
                {
                    // change move direction and move 
                    moveDirection *= -1;
                    Move();
                }
            }
        }


        private void Move()
        {
            ActionManager.onPlayerMoved?.Invoke();

            transform.position = new Vector2(transform.position.x + moveDirection * moveDistanceX, transform.position.y - moveDistanceY);
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Item"))
            {
                // effect
                StartCoroutine(ColorInvertEffect());
            
                // get time value from item
                int itemTime = other.gameObject.GetComponent<Item>().GetTime();
            
                ActionManager.onPlayerGetItem?.Invoke(itemTime);
            
                return;
            }

            // if move to stair
            if (other.gameObject.CompareTag("stair_in"))
            {
                // effect
                other.transform.parent.GetComponent<Stairs>().StairsEffectCoroutine();
                return;
            }
        
            // if move to empty space -> dead
            if (other.gameObject.CompareTag("stair_out"))
            {
                ActionManager.onPlayerOut?.Invoke();
                Dead();
            }
        }

        // countdown end? -> dead
        private void OnCountDownEnded()
        {
            Dead();
        }


        private void Dead()
        {
            isDead = true;

            // effect
            StartCoroutine(DeadEffect());
        }
    

        private IEnumerator DeadEffect()
        {
            // flicker effect ( Red And Black, 4 times ) 
            int flickerCount = 4;
            while (flickerCount > 0)
            {
                float waitTime = 0.08f;
                GetComponent<SpriteRenderer>().color = Color.red;
                yield return new WaitForSecondsRealtime(waitTime);
                GetComponent<SpriteRenderer>().color = Color.black;
                yield return new WaitForSecondsRealtime(waitTime);
                flickerCount--;
            }
        
            ActionManager.onPlayerExploded?.Invoke();
        
            GetComponent<SpriteRenderer>().enabled = false;
        
            // instantiate effect prefab and destroy after 1sec
            Destroy(Instantiate(fx_Dead, transform.position, Quaternion.identity), 1.0f);
        
            // wait 1sec to dead effect end.
            yield return new WaitForSecondsRealtime(1.0f);
        
            ActionManager.onGameEnded?.Invoke();
        }

        // if all stairs are made, set isStairsReady to true (call from StairsManager)
        public void SetToReady()
        {
            isStairsReady = true;
        }


        private IEnumerator ColorInvertEffect()
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            timeLimitText.color = Color.black;
        
            yield return new WaitForSeconds(.1f);
        
            GetComponent<SpriteRenderer>().color = Color.black;
            timeLimitText.color = Color.white;
        }
    
    
        public void SetDirection(int dir)
        {
            moveDirection = dir;
        }


        public bool IsStarted()
        {
            return isStarted;
        }
    
    
        public bool IsDead()
        {
            return isDead;
        }
    
  
    
    





    }
}
