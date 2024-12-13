using System;
using UnityEngine;

namespace _2D_BUNDLE.WallToWall2022.Scripts {
    public class PfChild : MonoBehaviour {
    
        public Action OnDestroyed;

        private void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }
    }
}
