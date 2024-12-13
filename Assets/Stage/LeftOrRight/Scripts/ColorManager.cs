using UnityEngine;
using Random = UnityEngine.Random;

namespace _2D_BUNDLE.LeftOrRight.Scripts {
    public class ColorManager : MonoBehaviour
    {
    
    
        private void Start()
        {
            ActionManager.onGameStarted += OnGameStarted;
        }

    
        private void OnDestroy()
        {
            ActionManager.onGameStarted -= OnGameStarted;
        }

    
        private void OnGameStarted()
        {
            SetBackgroundColor();
        }

    
        private void SetBackgroundColor()
        {
            float hueValueForBackgroundColor = Random.Range(0, 10) / 10.0f;
            Camera.main.backgroundColor = Color.HSVToRGB(hueValueForBackgroundColor, 0.6f, 0.8f);
        }


    }
}
