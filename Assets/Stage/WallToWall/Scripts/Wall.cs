using UnityEngine;

namespace _2D_BUNDLE.WallToWall2022.Scripts {
    public class Wall : MonoBehaviour
    {

        private void Start()
        {
            BallActionManager.Instance.onWallTouched += OnWallTouched;
        }

        private void OnWallTouched()
        {
            if (Ball.Instance.GetTouchedWall() == this)
            {
                GetComponent<ObstacleManager>().HandleObstacle();
            }
        }
    }
}
