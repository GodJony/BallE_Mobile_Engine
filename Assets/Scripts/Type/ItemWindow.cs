using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ItemWindow : MonoBehaviour
{
    /// <summary>
    /// 아이템 버튼 프리팹
    /// </summary>
    [SerializeField] private GameObject m_itemBasePrefab;

    /// <summary>
    /// 아이템 콘텐츠
    /// </summary>
    [SerializeField] private GameObject m_contents;

    public bool IsSkinFlag = false;

    /// <summary>
    /// 잠금 스프라이트
    /// </summary>
    public Sprite m_noSprite;

    public Navigation m_nav;

    /// <summary>
    /// 유저 스킨 리스트
    /// </summary>
    public List<UserData> IsallSkins { get; private set; } = new List<UserData>();

    /// <summary>
    /// 뷰포트
    /// </summary>
    [SerializeField] GameObject m_viewport;

    /// <summary>
    /// 유저 스킨 세팅
    /// </summary>
    /// <returns></returns>
    public IEnumerator SettingAllSkin()
    {
        IsSkinFlag = false;

        m_nav.m_navMenuList[0].SetActive(true);

        AsyncOperationHandle _handle;

        int _curIndex = 0;

        int _totalUserIndex = GManager.Instance.IsTotalIndex;


        IsallSkins = new List<UserData>();

        foreach (Transform child in m_contents.transform)
        {
            Destroy(child.gameObject);
        }   

        while (_curIndex < _totalUserIndex)
        {
            string _path = GManager.Instance.GetAdressablePath(_curIndex);

            _handle = Addressables.LoadAssetAsync<UserData>(_path);

            yield return _handle;

            if (_handle.Result != null)
            {
                UserData skinData = _handle.Result as UserData;

                if (skinData != null)
                {
                    IsallSkins.Add(skinData);
                }
                _curIndex++;
            }
        }

        ProcessAllSkins(IsallSkins);
        IsSkinFlag = true;
        m_nav.m_navMenuList[0].SetActive(false);
    }

    private void ProcessAllSkins(List<UserData> skins)
    {
        int _index = 0;

        foreach (var skin in skins)
        {
            GameObject _newItem = Instantiate(m_itemBasePrefab, m_contents.transform);

            _newItem.name = $"{skin.IsIndex}";
            SettingItem(_newItem, skin, _index);

            _index++;
        }
    }

    /// <summary>
    /// 이미지 세팅
    /// </summary>
    /// <param name="itemObj">아이템 오브젝트</param>
    /// <param name="argSkinData">유저 스킨 데이터</param>
    private void SettingItem(GameObject itemObj, UserData argSkinData, int argIndex)
    {
        Image _skinImage = itemObj.GetComponent<Image>();

        if (_skinImage != null && argSkinData.IsSprite != null)
        {
            _skinImage.sprite = argSkinData.IsSprite;
        }
    }

    /// <summary>
    /// 타이밍 문제 때문에 이렇게
    /// </summary>
    public void SetSkinLock()
    {
        Transform contentsTransform = m_viewport.transform.Find("Content");

        List<GameObject> _objList = new List<GameObject>();
        List<Image> _imgList = new List<Image>();

        foreach (Transform child in contentsTransform)
        {
            _objList.Add(child.gameObject);
        }

        for (int i = 0; i < GManager.Instance.IsTotalIndex; i++)
        {
            Image img = _objList[i].GetComponent<Image>();
            _imgList.Add(img);
            if (GManager.Instance.IsplayerSkinFlagArr[i]==false)
            {
                img.sprite = m_noSprite;                
            }
        }
    }


    /// <summary>
    /// 스프라이트 반환
    /// </summary>
    /// <param name="argIndex">인덱스</param>
    /// <returns></returns>
    public Sprite ReturnSprite(int argIndex)
    {
        return IsallSkins[argIndex].IsSprite;
    }
}
