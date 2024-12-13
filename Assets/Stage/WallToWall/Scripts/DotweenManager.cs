using DG.Tweening;
using UnityEngine;

namespace _2D_BUNDLE.WallToWall2022.Scripts {
    public class DotweenManager : MonoBehaviour
    {
        private void Awake()
        {
            DOTween.SetTweensCapacity(200, 125);
        }
    }
}
