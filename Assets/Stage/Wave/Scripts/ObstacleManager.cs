using UnityEngine;

namespace _2D_BUNDLE.Wave.Scripts {
    public class ObstacleManager : MonoBehaviour
    {

        [SerializeField] private GameObject player;
    
        // obstacle array 
        [SerializeField] private GameObject[] easyObstacleArray;
        [SerializeField] private GameObject[] normalObstacleArray;
    
        private int obstacleIndex = 0;
        private int distanceToNextObstacle = 50;

        private int playerPositionIndex = -1;


        private void Start()
        {
            InstantiateObstacle();
        }

        private void Update()
        {
            // When the y-position of the player increases, the InstantiateObstacle() is called.
            if (playerPositionIndex != (int)player.transform.position.y / 25)
            {
                InstantiateObstacle();
                playerPositionIndex = (int)player.transform.position.y / 25;
            }
        }


        private void InstantiateObstacle()
        {
            // get score
            int score = ScoreManager.Instance.GetScore();
        
            // if the score is greater than 20, the game becomes more difficult.
            if (score < 20)
            {
                int randomObstacle = Random.Range(0, easyObstacleArray.Length);
                GameObject newObstacle = Instantiate(easyObstacleArray[randomObstacle], new Vector3(0, obstacleIndex * distanceToNextObstacle), Quaternion.identity);
                newObstacle.transform.SetParent(transform);
            }
            else
            {
                int randomObstacle = Random.Range(0, normalObstacleArray.Length);
                GameObject newObstacle = Instantiate(normalObstacleArray[randomObstacle], new Vector3(0, obstacleIndex * distanceToNextObstacle), Quaternion.identity);
                newObstacle.transform.SetParent(transform);
            }

            obstacleIndex++;
        }
    }
}