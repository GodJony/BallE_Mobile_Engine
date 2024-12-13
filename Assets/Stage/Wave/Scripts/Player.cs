using UnityEngine;
using _2D_BUNDLE.Wave.Scripts;

namespace _2D_BUNDLE.Wave.Scripts {
    public class Player : MonoBehaviour
    {
    
        [SerializeField] private GameObject gameManager;
    
        private Rigidbody2D rb;
        private float angle = 0;

        private float mapWidth;
        [SerializeField] private float xSpeed;
        [SerializeField] private float ySpeed;
        [SerializeField] private int ySpeedMax;
        [SerializeField] private int yAccelerationForce;
        [SerializeField] private int yDecelerationForce;

        private bool isDead = false;

        [SerializeField] private GameObject fx_Dead;
        [SerializeField] private GameObject fx_ColorChange;

        private AudioSource source;
        [SerializeField] private AudioClip DeadClip;
        [SerializeField] private AudioClip ItemClip;
    
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            source = GetComponent<AudioSource>();
        
            mapWidth = gameManager.GetComponent<DisplayManager>().GetWidth();
        }


        private void Update()
        {
            if (isDead) return;
        
            MovePlayer();
        }


        private void MovePlayer()
        {
            Vector2 position = transform.position;
        
            // Move from side to side.
            position.x = Mathf.Cos(angle) * (mapWidth * 0.45f);
        
            // Move forward little by little.
            position.y += ySpeed * Time.deltaTime;
        
            transform.position = position;
        
            // increase angle
            angle += Time.deltaTime * xSpeed;

        
            if (Input.GetMouseButton(0))
            {
                if (rb.velocity.y < ySpeedMax)
                {
                    rb.AddForce(new Vector2(0, yAccelerationForce));
                }
            }
            else
            {
                if (rb.velocity.y > 0)
                {
                    rb.AddForce(new Vector2(0, -yDecelerationForce));
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                }
            }

        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Item_ColorChange"))
            {
                // display item effect and destroy after 0.5sec
                GameObject itemFxGameObject = Instantiate(fx_ColorChange, other.gameObject.transform.position, Quaternion.identity); 
                Destroy(itemFxGameObject, 0.5f);
            
                // destroy 'other' game object's parent
                Destroy(other.gameObject.transform.parent.gameObject);
            
                // change background color
                ColorManager.Instance.ChangeBackgroundColor();

                // add score
                ScoreManager.Instance.AddScore();

                // play audio
                source.PlayOneShot(ItemClip, 1);
            }

            if (other.gameObject.CompareTag("Obstacle") && isDead == false)
            {
                isDead = true;
            
                // display dead effect and destroy after 0.5sec
                GameObject deadFx = Instantiate(fx_Dead, transform.position, Quaternion.identity); 
                Destroy(deadFx, 0.5f);
            
                // stop player
                rb.velocity = new Vector2(0, 0);
                rb.isKinematic = true;
            
                //set game over
                gameManager.GetComponent<WaveGameManager>().GameOver();
            
                // play audio
                source.PlayOneShot(DeadClip, 1);
            }
        }
    }
}
