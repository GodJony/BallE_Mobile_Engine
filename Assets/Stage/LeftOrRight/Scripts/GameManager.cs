using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _2D_BUNDLE.LeftOrRight.Scripts {
    public class GameManager : MonoBehaviour {
    
    
    

        private IEnumerator Start()
        {
            // wait to assign method to 'onGameStarted' action at other script  ex) UiManager.cs
            // wait 1 frame and invoke action
            yield return 0;
            ActionManager.onGameStarted?.Invoke();
        }

    
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


    }
}
