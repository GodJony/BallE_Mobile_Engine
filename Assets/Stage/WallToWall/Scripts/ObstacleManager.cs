using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _2D_BUNDLE.WallToWall2022.Scripts {
    public class ObstacleManager : MonoBehaviour {

        [SerializeField] private GameObject pfObstacle;
        [SerializeField] private Transform obstaclePositionParent;

        private List<Transform> obstaclePositionList;

        private void Awake()
        {
            obstaclePositionList = new List<Transform>();
            foreach (Transform position in obstaclePositionParent)
            {
                obstaclePositionList.Add(position);
            }
        }

        private void Start()
        {
            StartCoroutine(CreateNewObstacle());
        }

        IEnumerator CreateNewObstacle()
        {
            List<Transform> newObstaclePositionList = obstaclePositionList.ToList();
            int emptyCount = Random.Range(5, 10);
            int obstacleCount = obstaclePositionList.Count - emptyCount;
        
            for (int i = 0; i < obstacleCount; i++)
            {
                int randomIndex = Random.Range(0, newObstaclePositionList.Count);
            
                GameObject newObstacle = Instantiate(pfObstacle, newObstaclePositionList[randomIndex].position, Quaternion.identity);
                newObstacle.transform.SetParent(transform);
                WallTypeHolder.WallType currentWallType = GetComponent<WallTypeHolder>().wallType; 
                switch (currentWallType)
                {
                    case WallTypeHolder.WallType.LEFT:
                        newObstacle.transform.eulerAngles = new Vector3(0, 0, 0);
                        break;
                    case  WallTypeHolder.WallType.RIGHT:
                        newObstacle.transform.eulerAngles = new Vector3(0, 180, 0);
                        break;
                }
            
                newObstaclePositionList.RemoveAt(randomIndex);
            
                yield return new WaitForSeconds(.1f);
            }
        }

        public void HandleObstacle()
        {
            if (HasObstacle())
            {
                DestroyOldObstacle();
            }
            StartCoroutine(CreateNewObstacle());
        }

        private bool HasObstacle()
        {
            return transform.childCount != 0;
        }

        private void DestroyOldObstacle()
        {
            foreach (Transform oldObstacle in transform)
            {
                oldObstacle.GetComponentInChildren<Obstacle>().DestroyEffect();
            }
        }

    }
}
