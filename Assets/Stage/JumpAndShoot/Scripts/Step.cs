using System.Collections;
using UnityEngine;

namespace _2D_BUNDLE.JumpAndShoot.Scripts {
    public class Step : MonoBehaviour
    {
        private float _distance = 3;
        private float _velocity = 2;

        private float _angle;   

    

        void Update()
        {
            // If the game is playing,  step move
            if (Time.timeScale == 1)
            {
                MoveSideToSide();
            };
        }


        void MoveSideToSide()
        {
            transform.position = new Vector2(Mathf.Sin(_angle) * _distance, transform.position.y);
            _angle += Time.deltaTime*_velocity;
        }


        public void LandingEffect()
        {
            StartCoroutine(LandingEffectCoroutine());
        }


        public IEnumerator LandingEffectCoroutine()
        {
            // save current position
            float originalPositionY = transform.position.y;
        
            // Step moves as much as the YChangeValue.
            float yMoveDistance = 1.5f;

            while (yMoveDistance > 0)
            {
                yMoveDistance -= 0.1f;
                transform.position = new Vector3(transform.position.x, originalPositionY - yMoveDistance);
                yield return 0;
            }
        }

    
        public void SetDistance(float value)
        {
            _distance = value;
        }
    
    
        public void SetVelocity(float value)
        {
            _velocity = value;
        }



    }
}
