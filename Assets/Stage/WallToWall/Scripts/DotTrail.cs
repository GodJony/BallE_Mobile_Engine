using System.Collections;
using UnityEngine;

namespace _2D_BUNDLE.WallToWall2022.Scripts {
    public class DotTrail : MonoBehaviour {
    
    
        [SerializeField] private GameObject pfDot;

        private void Start()
        {
            StartCoroutine("DrawDotCoroutine");
        }

        private IEnumerator DrawDotCoroutine()
        {
            while (true)
            {
                Instantiate(pfDot, transform.position, Quaternion.identity);
                float interval = .1f;
                yield return new WaitForSeconds(interval);
            }
        }

    }
}
