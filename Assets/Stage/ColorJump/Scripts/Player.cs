using UnityEngine;

namespace _2D_BUNDLE.ColorJump.Scripts {
    public class Player : MonoBehaviour
    {
    
        // effect
        [SerializeField] private GameObject fx_Jump;
        [SerializeField] private GameObject fx_StepDestroy;
        [SerializeField] private GameObject fx_Dead;
    
        // audio
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip jumpClip;
        [SerializeField] private AudioClip deadClip;
    
        // gravity value
        [SerializeField] private float gravity;
        [SerializeField] private float maxGravity;
        [SerializeField] private float gravityIncrease;
    
    
        private Rigidbody2D rb2D;

        private Vector2 playerPosition;
        private Vector2 lastTouchedPosition;
        private Vector2 currentTouchedPosition;
    
        private bool isStart = false;
        private bool isDead = false;
        private bool isTouching = false;

    
        private float jumpVelocity;


        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();

        }

    
        void Update()
        {
            // when mouse button down, game start
            if (isStart == false && Input.GetMouseButtonDown(0))
            { 
                isStart = true;
                isTouching = true;
            
                // save touched position and current player position to move player
                lastTouchedPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                playerPosition = transform.position;
            
                EffectManager.Instance.StartGame();
            }

            // If the game is playing, and the player is not dead yet,
            if (isStart == true && isDead == false)
            {
                if (isTouching == true)
                {
                    // move player
                    currentTouchedPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                    float movedPositionX = currentTouchedPosition.x - lastTouchedPosition.x;
                    float xMoveSpeed = 1.5f;
                    transform.position = new Vector3(playerPosition.x + movedPositionX * xMoveSpeed, transform.position.y);

                    // Limits the range of y-axis movement.
                    if (transform.position.x < DisplayManager.Instance.GetLeft())
                    {
                        transform.position = new Vector3(DisplayManager.Instance.GetLeft(), transform.position.y);
                    }
                    if (transform.position.x > DisplayManager.Instance.GetRight())
                    {
                        transform.position = new Vector3(DisplayManager.Instance.GetRight(), transform.position.y);
                    }
                }
            
                // add custom gravity to player
                rb2D.velocity = new Vector2(0, rb2D.velocity.y - (gravity * gravity));
            
                // If the player falls off the screen, game over
                if (isDead == false && Camera.main.transform.position.y - transform.position.y > DisplayManager.Instance.GetHeight() / 2)
                {
                    isDead = true;
                    rb2D.isKinematic = true;
                    rb2D.velocity = Vector2.zero;
                    GameOver();
                }
            
            }
        
        
            if (Input.GetMouseButtonUp(0)) 
            {
                isTouching = false; 
            }
        
        }
    

        void GameOver()
        {
            GManager.Instance.IsGameOverFlag = true;
            Instantiate(fx_Dead, transform.position, Quaternion.identity);
        
            audioSource.PlayOneShot(deadClip, 1);
        }


        void OnTriggerEnter2D(Collider2D other)
        {
            // if 'other' is step  && if player is falling down,
            if (other.gameObject.CompareTag("Step") && rb2D.velocity.y <= 0)
            { 
                // jump the player
                Jump();
                // start effect
                Effect(other);
                // change background color using 'other' step's color
                ChangeBackgroundColor(other);
                // destroy 'other' step, create new step
                DestroyAndCreateNewStep(other);
                // increase gravity
                IncreaseGravity();
                // add score
                ScoreManager.Instance.AddScore(1);
                //play audio clip
                audioSource.PlayOneShot(jumpClip, 1);
            }
        }
    

        void Jump()
        {
            float jumpValue = 28f;
            jumpVelocity = gravity * jumpValue;
            rb2D.velocity = new Vector2(0, jumpVelocity);
        }

    
        void Effect(Collider2D step)
        {
            GameObject jumpEffect = Instantiate(fx_Jump, transform.position, Quaternion.identity);
            Destroy(jumpEffect, 1.0f);

            GameObject stepDestroyEffect = Instantiate(fx_StepDestroy, step.gameObject.transform.position, Quaternion.identity);
            Destroy(stepDestroyEffect, 0.5f);
        }

    
        void DestroyAndCreateNewStep(Collider2D step)
        {
            Destroy(step.gameObject);
            StepManager.Instance.MakeNewStep();
        }
    
    
        void ChangeBackgroundColor(Collider2D step)
        {
            Color stepColor = step.gameObject.GetComponent<SpriteRenderer>().color;
            Color.RGBToHSV(stepColor, out float h, out float s, out float v );
        
            Camera.main.backgroundColor = Color.HSVToRGB(h, s - 0.15f, v - 0.15f);
        }
        
        // increase the gravity if it is less than maxGravity.
        void IncreaseGravity()
        {
            gravity += gravityIncrease;
            if (gravity > maxGravity) gravity = maxGravity;
        }

    }
}
