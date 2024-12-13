using UnityEngine;

namespace _2D_BUNDLE.Wave.Scripts {
    public class ObstacleMovement : MonoBehaviour
    {

        [SerializeField] private float scaleSizeValue;

        [SerializeField] private float scaleChangeSpeedX;
        [SerializeField] private float scaleChangeSpeedY;

        [SerializeField] private float rotationSpeed; 

        [Space(10)]
        [SerializeField] private float angleForSideMove = 0;
        [SerializeField] private float sideMoveDistance;
        [SerializeField] private float sideMoveSpeed;

        [Space(10)]
        [SerializeField] private float angleForUpDownMove = 0;
        [SerializeField] private float upDownMoveDistance;
        [SerializeField] private float upDownMoveSpeed;
    
        private float angleX = 0;
        private float angleY = 0;
    
        private float originalScaleX;
        private float originalScaleY;
        private Vector2 originalPosition;


        void Start()
        {
            originalScaleX = transform.localScale.x;
            originalScaleY = transform.localScale.y;
            originalPosition = transform.position;
        }


        void Update()
        {
            if (scaleChangeSpeedX != 0 || scaleChangeSpeedY != 0)
            {
                ScaleChange();
            }

            if (rotationSpeed != 0)
            {
                AngleChange();
            }

            if (sideMoveSpeed != 0)
            {
                SideToSidePositionChange();
            }

            if (upDownMoveDistance != 0)
            {
                UpDownPositionChange();
            }
        }

        void ScaleChange()
        {
            transform.localScale = new Vector2(originalScaleX + Mathf.Sin(angleX) * scaleSizeValue, originalScaleY + Mathf.Sin(angleY) * scaleSizeValue);
            angleX += Time.deltaTime * scaleChangeSpeedX;
            angleY += Time.deltaTime * scaleChangeSpeedY;
        }


        void AngleChange()
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed, Space.World);
        }


        void SideToSidePositionChange()
        {
            Vector2 pos = transform.position;
            pos.x = originalPosition.x + Mathf.Sin(angleForSideMove) * sideMoveDistance;
            transform.position = pos;

            angleForSideMove += Time.deltaTime * sideMoveSpeed;
        }

        void UpDownPositionChange()
        {
            Vector2 pos = transform.position;
            pos.y = originalPosition.y + Mathf.Sin(angleForUpDownMove) * upDownMoveDistance;
            transform.position = pos;

            angleForUpDownMove += Time.deltaTime * upDownMoveSpeed;
        }
    }
}
