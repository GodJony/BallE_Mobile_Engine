using System.Collections;
using UnityEngine;

namespace _2D_BUNDLE.JumpAndShoot.Scripts {
    public class Player : MonoBehaviour
    {
        enum PlayerState
        {
            Landing, Jumping, Shooting
        }
        PlayerState _currentState;

        public Transform playerParentTransform;

  
        [Space]
        public GameObject fx_Shoot_1;
        public GameObject fx_Shoot_2;
        public GameObject fx_Jump;
        public GameObject fx_Land;
        public GameObject fx_StepDestroy;
        public GameObject fx_Dead;
        [Space]
        public int jumpSpeed;
        public int shootSpeed;

        private Rigidbody2D _rb2D;
        private BoxCollider2D _boxCollider2D;

        private float _previousPosXofParent;
        private float _hueValue;
        private bool _isDead = false;
    
        private float _leftWallPosition;
        private float _rightWallPosition;


    
        void Awake()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
        
            // get left, right wall position
            _leftWallPosition = GetDisplayBound.Instance.GetLeftWallPosition();
            _rightWallPosition = GetDisplayBound.Instance.GetRightWallPosition();
        }


    
        void Start()
        {
            _currentState = PlayerState.Shooting;
            _rb2D.velocity = new Vector2(0, 0);
        }

    

        void Update()
        {
            // ------------------------------------------------------------ Get Input ------------------------------------------------------------
            if (Input.GetMouseButtonDown(0))
            {
                if (_currentState == PlayerState.Landing) 
                {
                    Jump();
                }
                else if (_currentState == PlayerState.Jumping) 
                {
                    StartCoroutine(Shoot());
                }
            }
        
            // ------------------------------------------------------------ Bounce At Wall ------------------------------------------------------------ //
            if (_rb2D.position.x < _leftWallPosition)
            {
                _rb2D.position = new Vector2(_leftWallPosition, _rb2D.position.y);
                _rb2D.velocity = new Vector2(-_rb2D.velocity.x, _rb2D.velocity.y);
            }
            if (_rb2D.position.x > _rightWallPosition)
            {
                _rb2D.position = new Vector2(_rightWallPosition, _rb2D.position.y);
                _rb2D.velocity = new Vector2(-_rb2D.velocity.x, _rb2D.velocity.y);
            }
        
            // ------------------------------------------------------------ Dead Check ------------------------------------------------------------ //
            if (_isDead == false && Camera.main.transform.position.y - transform.position.y > 10)
            {
                _isDead = true;
                _rb2D.isKinematic = true;
                _rb2D.velocity = Vector2.zero;

                Destroy(Instantiate(fx_Dead, transform.position, Quaternion.identity), 1.0f);
            
                UIManager.Instance.GameOver();
            }

            // Save current X position of parent to calculate velocity when jumping
            _previousPosXofParent = transform.parent.transform.position.x;

            // When jumping, rotate the player
            if (_currentState == PlayerState.Jumping)
            {
                transform.Rotate(Vector3.forward * (Time.deltaTime * _rb2D.velocity.x * (-30)));
            }
        }
    


        void Jump()
        {
            JumpEffect();
        
            // Calculates the velocity of the parent object.
            float parentVelocity = (transform.parent.transform.position.x - _previousPosXofParent) / Time.deltaTime;
        
            // Jump the player
            _rb2D.velocity = new Vector2(parentVelocity, jumpSpeed);
        
            // Change current state to JUMPING
            _currentState = PlayerState.Jumping;

            // Disable boxCollider to avoid hitting the step while the player is jumping
            _boxCollider2D.enabled = false;

            // Set parent to default
            transform.SetParent(playerParentTransform);
        }
    
    
    
        IEnumerator Shoot()
        {
            // Set rotation to zero
            transform.rotation = Quaternion.identity;

            // Change current state to Shooting
            _currentState = PlayerState.Shooting;

            //Effect1
            ShootEffect1();
        
            // Pause in the air before shooting.
            _rb2D.isKinematic = true;
            _rb2D.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(0.5f);

            // Effect2
            ShootEffect2();
        
            // Change background color
            ColorManager.Instance.ChangeBackgroundColor();

            // SHOOT!
            _rb2D.isKinematic = false;
            _rb2D.velocity = new Vector2(0, -shootSpeed);

            // Enable box collider 
            _boxCollider2D.enabled = true;
        }
    
    
    
        void JumpEffect()
        {
            GameObject effectObj = Instantiate(fx_Jump, transform.position, Quaternion.identity);
            Destroy(effectObj, 0.5f);
        }

    
    
        void ShootEffect1()
        {
            GameObject tempObj = Instantiate(fx_Shoot_1, transform.position, Quaternion.identity);
            float currentHue = ColorManager.Instance.GetCurrentHue();
            tempObj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.HSVToRGB(currentHue, 0.6f, 0.8f);
            Destroy(tempObj, 1.0f);
        }

    
    
        void ShootEffect2()
        {
            GameObject EffectObj = Instantiate(fx_Shoot_2, transform.position, Quaternion.identity);
            Destroy(EffectObj, 0.5f);
        }


    
        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Step") && _currentState == PlayerState.Shooting)
            {
                // Instantiate landingEffect and destroy it in 0.5 second 
                GameObject landingEffect = Instantiate(fx_Land, transform.position, Quaternion.identity); 
                Destroy(landingEffect, 0.5f);
            
                // Stop the player
                _rb2D.velocity = new Vector2(0, 0);
            
                // Change current state to Landing
                _currentState = PlayerState.Landing;

                // Change parent of player to current step
                transform.SetParent(other.gameObject.transform);

                // Start Landing Effect
                other.gameObject.GetComponent<Step>().LandingEffect();
            
                // Add Score
                ScoreManager.Instance.AddScore(1);
            }
        }

        // When player jumping
        void OnCollisionExit2D(Collision2D other)
        {
            StepManager.Instance.CreateNewStep();
        
            DestroyCurrentStep(other);
        }


        void DestroyCurrentStep(Collision2D stepCollision)
        {
            // Destroy current step in 0.1 sec
            Destroy(stepCollision.gameObject, 0.1f);

            // Instance step destroy effect
            GameObject fxObj = Instantiate(fx_StepDestroy, stepCollision.gameObject.transform.position, Quaternion.identity);
            // set scale of effect object
            fxObj.transform.localScale = stepCollision.transform.localScale;
            // destroy effect in 0.5 sec
            Destroy(fxObj, 0.5f);
        }




    }
}
