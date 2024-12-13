using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _2D_BUNDLE.LeftOrRight.Scripts {
    public class Item : MonoBehaviour
    {
    
        private int time;

        [SerializeField] private TextMeshPro timeText;
    
        [SerializeField] private GameObject fxItemParticle;
        [SerializeField] private GameObject fxItemText;
    


        private void Start()
        {
            // set 'time' of this item
            SetTime();
        
            ActionManager.onPlayerGetItem += OnPlayerGetItem;
        }

        private void OnDestroy()
        {
            ActionManager.onPlayerGetItem -= OnPlayerGetItem;
        }


        private void SetTime()
        {
            // 'time' will be 1,2,3 or 4
            int randomInt = Random.Range(0, 10); // 0-9
            if (randomInt >= 0 && randomInt < 6) time = 1; //0-5 60%
            if (randomInt >= 6 && randomInt < 9) time = 2; //6-8 30% 
            if (randomInt >= 9) time = 3; // 9 10%
        
            // update text
            timeText.text = time.ToString();
        }

    
        private void OnPlayerGetItem(int notUse)
        {
            // start effect
            TextEffect();
            ParticleEffect();
        
            // destroy parent of this gameobject
            Destroy(transform.parent.gameObject);
        }
    
    
        private void TextEffect()
        { 
            // instantiate prefab
            GameObject fxText = Instantiate(fxItemText, transform.position, Quaternion.identity);
        
            // set text
            string text = "Second";
            if (time != 1) text += "s";
            fxText.transform.Find("timeText").GetComponent<TextMeshPro>().text = "+" + time + " " + text;
       
            // destroy after 1 sec
            Destroy(fxText, 1.0f);
        }

        private void ParticleEffect()
        {
            // instantiate prefab and destroy after 1 sec
            Destroy(Instantiate(fxItemParticle, transform.position, Quaternion.identity), 1.0f);
        }
    
    
        public int GetTime()
        {
            return time;
        }


    }
}
