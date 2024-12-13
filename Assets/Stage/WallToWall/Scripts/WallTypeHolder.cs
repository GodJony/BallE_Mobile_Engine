using UnityEngine;

namespace _2D_BUNDLE.WallToWall2022.Scripts {
    public class WallTypeHolder : MonoBehaviour
    {
        public enum WallType {
            LEFT, RIGHT, TOP, BOTTOM
        }

        public WallType wallType;
    }
}
