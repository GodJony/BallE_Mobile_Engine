using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class User : MonoBehaviour
{
    /// <summary>
    /// ���� ������
    /// </summary>
    public UserData IsUserData { get; private set; }

    /// <summary>
    /// ��������Ʈ
    /// </summary>
    private Sprite m_sprite = null;

    /// <summary>
    /// �ε���
    /// </summary>
    private int m_index = 0;

    /// <summary>
    /// ����
    /// </summary>
    /// <returns>���� ��Ų ������</returns>
    public IEnumerator Setting()
    {
        GManager.Instance.IsSettingFlag = false;

        GManager.Instance.IsPlayerObj = GameObject.Find("player");

        m_index = PlayerPrefs.GetInt("Index",0);
        string _path = GManager.Instance.GetAdressablePath(m_index);
        AsyncOperationHandle _handle = Addressables.LoadAssetAsync<UserData>(_path);

        yield return _handle;

        if (_handle.Result != null)
        {
            IsUserData = _handle.Result as UserData;

            if(GManager.Instance.IsPlayerObj == null) GManager.Instance.IsPlayerObj = GameObject.Find("player");


            if (IsUserData != null && GManager.Instance.IsPlayerObj != null)
            {

                m_sprite = IsUserData.IsSprite;

                switch (GManager.Instance.IsSceneType)
                {
                    case SceneType.TYPE.WallToWall:
                        BallGenerator _ballGenerator = FindObjectOfType<BallGenerator>();
                        if (_ballGenerator != null)
                        {
                            _ballGenerator.SetBallSprite(m_sprite);
                        }
                        break;
                    default:
                        SpriteRenderer _sprRender = GManager.Instance.IsPlayerObj.GetComponent<SpriteRenderer>();

                        if (_sprRender != null)
                        {
                            _sprRender.sprite = m_sprite;
                        }
                        break;
                }
            }

        }

        Addressables.Release(_handle);

        GManager.Instance.IsSettingFlag = true;
    }
}
