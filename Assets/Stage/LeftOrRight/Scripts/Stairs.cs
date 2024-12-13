using System.Collections;
using UnityEngine;

namespace _2D_BUNDLE.LeftOrRight.Scripts {
    public class Stairs : MonoBehaviour
    {

        // if player move to this stair, this method called.
        public void StairsEffectCoroutine()
        {
            StartCoroutine(StairsEffect());
        }
    
    
        private IEnumerator StairsEffect()
        {
            float xValue = transform.localScale.x / 50f;
            float yValue = transform.localScale.y / 50f;
        
            Vector2 originalScale = transform.localScale;

            // set local scale to 150% of original scale
            transform.localScale = new Vector2(transform.localScale.x + transform.localScale.x / 2f, transform.localScale.y + transform.localScale.y / 2f);
        
            // decrease local scale to original scale. 
            while (transform.localScale.x > originalScale.x)
            {
                transform.localScale = new Vector2(transform.localScale.x - xValue, transform.localScale.y - yValue);
                yield return 0;
            }
        }


    }
}
