using System.Collections;
using UnityEngine;

namespace _2D_BUNDLE.Wave.Scripts {
    public class ObstacleParent : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(CalculateDistance());
        }
    

        // If the distance from the player becomes distant, destroy this game object.
        private IEnumerator CalculateDistance()
        {
            while (true)
            {
                if (GameObject.Find("player").transform.position.y - transform.position.y > 60)
                {
                    Destroy(gameObject);
                }
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}