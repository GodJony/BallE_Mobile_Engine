using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _2D_BUNDLE.LeftOrRight.Scripts {
    public class StairsManager : MonoBehaviour
    {


        [SerializeField] private GameObject player;

        [SerializeField] private GameObject stairsPrefab;

        [Range(2, 15)]
        [SerializeField] private int numberOfStairs = 15;

        [SerializeField] private float stairsWidth = 3f;
        [SerializeField] private float stairsHeight = 0.5f;

        [SerializeField] private float distanceX = 1.5f;
        [SerializeField] private float distanceY = 2.5f; 

        private List<Transform> childTransformList;

    
        private void Start()
        {
            ActionManager.onPlayerMoved += OnPlayerMoved;
            MakeStairs();
            StartCoroutine(SetStairsPosition());
        }

        private void OnDestroy()
        {
            ActionManager.onPlayerMoved -= OnPlayerMoved;
        }

        private void OnPlayerMoved()
        {
            StairsReposition();
        }


        private void MakeStairs()
        {
            childTransformList = new List<Transform>();

            for (int i = 0; i < numberOfStairs; i++)
            {
                GameObject stairsObj = Instantiate(stairsPrefab, Vector2.zero, Quaternion.identity);
                stairsObj.transform.SetParent(transform);
                stairsObj.transform.position = new Vector2(-99999, -99999);
                childTransformList.Add(stairsObj.transform);
            }

        }


        private IEnumerator SetStairsPosition()
        {
            childTransformList[0].position = new Vector3(0, -player.transform.localScale.y / 2f - stairsHeight / 2f);
            childTransformList[0].localScale = new Vector2(stairsWidth, stairsHeight);

            for (int i = 1; i < transform.childCount; i++)
            {
                childTransformList[i].localScale = new Vector2(stairsWidth, stairsHeight);
                childTransformList[i].position = childTransformList[i - 1].transform.position + new Vector3(distanceX - Random.Range(0, 2) * distanceX * 2, -distanceY);
                yield return new WaitForSecondsRealtime(0.07f);
            }

            player.GetComponent<Player>().SetToReady();
            SetFirstDirection();
            GameObject.Find("itemManager").GetComponent<ItemManager>().MakeNewItem();
        }


        private void StairsReposition()
        {
            Transform firstTransform = childTransformList[0];
            firstTransform.position = childTransformList[transform.childCount - 1].transform.position + new Vector3(distanceX - Random.Range(0, 2) * distanceX * 2, -distanceY);

            childTransformList.RemoveAt(0);
            childTransformList.Add(firstTransform);
        }


        private void SetFirstDirection()
        {
            if (childTransformList[0].position.x > childTransformList[1].position.x)
            {
                player.GetComponent<Player>().SetDirection(-1);
            }
            else
            {
                player.GetComponent<Player>().SetDirection(1);
            }
        }


        public Vector3 GetNextStairsForItem()
        {
            int scoreValue = ScoreManager.Instance.GetCurrentScore() / 15;
            int randomInt = Random.Range(2, 6 + scoreValue);
            return childTransformList[randomInt].position;
        }

    
        public float GetStairHeight()
        {
            return stairsHeight;
        }
    
        public (float x, float y) GetDistance()
        {
            return (distanceX, distanceY);
        }


    }
}
