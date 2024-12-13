using UnityEngine;

namespace _2D_BUNDLE.WallToWall2022.Scripts {
    public class PfParent : MonoBehaviour {
    
        private PfChild[] pfChildArray;
        private int pfChildArrayLength;

        private void Awake()
        {
            pfChildArray = GetComponentsInChildren<PfChild>();
            pfChildArrayLength = pfChildArray.Length;
        
            foreach (PfChild pfChild in pfChildArray)
            {
                pfChild.OnDestroyed += OnDestroyed;      
            }

        }

        private void OnDestroyed()
        {
            pfChildArrayLength--;
            if (pfChildArrayLength == 0)
            {
                Destroy(gameObject);
            }
        }

    }
}
