using UnityEngine;

// Afterimage shows the afterimage effect when the player moves.
namespace _2D_BUNDLE.LeftOrRight.Scripts {
    public class Afterimage : MonoBehaviour {
    
        [SerializeField] private GameObject player;
    
        // array of afterimage objects
        [SerializeField] private GameObject[] afterimageArray;
        // array of velocity in afterimage
        private Vector3[] afterimageVelocityArray;
        // variables for SmoothDamp
        private float smoothTime = 0.04F;


        private void Start()
        {
            afterimageVelocityArray =  new Vector3[afterimageArray.Length];
        
            ActionManager.onPlayerOut += OnPlayerOut;
            ActionManager.onCountdownEnded += OnCountDownEnded;
        }


        private void OnDestroy()
        {
            ActionManager.onPlayerOut -= OnPlayerOut;
            ActionManager.onCountdownEnded -= OnCountDownEnded;
        }


        private void Update()
        {
            // Calculate the distance between the player and the last object in the afterimageArray
            float distance = Vector2.Distance(player.transform.position, afterimageArray[afterimageArray.Length-1].transform.position);
        
            if (distance > 0.01f)
            {
                Move();
            }
        }


        private void Move()
        {
            // AfterimageArray's first object moves along the player
            afterimageArray[0].transform.position = Vector3.SmoothDamp(afterimageArray[0].transform.position, player.transform.position, ref afterimageVelocityArray[0], smoothTime);
        
            // AfterimageArray[i] moves along each afterimageArray[i-1].
            for (int i = 1; i < afterimageArray.Length; i++)
            {
                afterimageArray[i].transform.position = Vector3.SmoothDamp(afterimageArray[i].transform.position, afterimageArray[i-1].transform.position, ref afterimageVelocityArray[i], smoothTime); 
            }
        }


        private void OnPlayerOut()
        {
            Hide();
        }

        private void OnCountDownEnded()
        {
            Hide();
        }


        private void Hide()
        {
            foreach (GameObject afterimage in afterimageArray)
            {
                afterimage.SetActive(false);
            }
        }
    }
}
