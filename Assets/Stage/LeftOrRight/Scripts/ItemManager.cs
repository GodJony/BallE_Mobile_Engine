using UnityEngine;

namespace _2D_BUNDLE.LeftOrRight.Scripts {
    public class ItemManager : MonoBehaviour
    {
    
        [SerializeField]private GameObject itemPrefab;
        [SerializeField]private StairsManager stairsManager;

    
        private void Start()
        {
            ActionManager.onPlayerGetItem += OnPlayerGetItem;
        }

        private void OnDestroy()
        {
            ActionManager.onPlayerGetItem -= OnPlayerGetItem;
        }

        private void OnPlayerGetItem(int notUse)
        {
            MakeNewItem();
        }


        public void MakeNewItem()
        {
            Vector2 pos = stairsManager.GetNextStairsForItem() + new Vector3(0, stairsManager.GetStairHeight()/2+1f, 0);
            GameObject itemObj =  Instantiate(itemPrefab, pos, Quaternion.identity);
            itemObj.transform.SetParent(transform);
        }

    }
}
