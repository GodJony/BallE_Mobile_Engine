using _2D_BUNDLE.JumpAndShoot.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GManager : MonoBehaviour
{
    //��� �� �ڵ带 ���� ������..? Hentai...Ű��...

    private string m_format = "Assets/Prefab/Data/UserData/{0:D}.asset";

    /// <summary>
    /// ����
    /// </summary>
    public User Isuser { get; private set; }  = null;


    /// <summary>
    /// �÷��̾� ������Ʈ
    /// </summary>
    public GameObject IsPlayerObj { get;  set; } = null;

    /// <summary>
    /// ��ü ���� ��Ų ������
    /// </summary>
    public int IsTotalIndex { get; private set; } = 10;

    /// <summary>
    /// ���� �ε���
    /// </summary>
    public int m_userIndex = -1;

    public SoundManager m_soundManager;

    /// <summary>
    /// ���� �÷���
    /// </summary>
    public bool IsSettingFlag = false;

    /// <summary>
    /// ��� �������� ��Ż ���ھ�
    /// </summary>
    public int IsTotalScore { get; private set; } = 0;

    /// <summary>
    /// ���� �� Ÿ��
    /// </summary>
    public SceneType.TYPE IsSceneType { get; private set; } = SceneType.TYPE.Title;

    /// <summary>
    /// �� �̵� �÷���
    /// </summary>
    public bool IschangeSceneFlag { get; private set; } = false;

    /// <summary>
    /// ���� �÷���
    /// false : ���� ��, ttue : ���� �Ϸ�
    /// </summary>
    public bool IsPrefSaveFlag { get; private set; } = false;

    /// <summary>
    /// UI �޴��� �̱��� ����
    /// </summary>
    public MyUIManager IsUIManager { get; private set; } = null;

    /// <summary>
    /// ���� ���� �÷���
    /// </summary>
    public bool IsGameOverFlag { get; set; } = false;

    public static GManager Instance { get; set; } = null;

    /// <summary>
    /// �׺���̼ǰ� �޴� ������Ʈ
    /// 1�� : �׺�
    /// 2�� : �޴�
    public List<GameObject> m_NavAndMenu;

    public Navigation m_navigation;

    public ItemWindow m_itemWidow;

    /// <summary>
    /// �ر� ���ھ� üũ��
    /// </summary>
    private int[] m_openScoreArr;

    /// <summary>
    /// �÷��̾� ��Ų �迭
    /// </summary>
    private int[] m_playerSkinArr;

    /// <summary>
    /// �÷��̾ �ش� ��Ų�� �ر��ߴ����� ���� �迭
    /// </summary>
    public bool[] IsplayerSkinFlagArr { get; private set; }

    private void Awake()
    {
        m_soundManager.PlayBGM(SceneType.TYPE.Title);

        if (GManager.Instance == null)
        {
            Instance = this;
            IsUIManager = transform.Find("UIManager").GetComponent<MyUIManager>();
            IsSceneType = SceneType.TYPE.Title;
            DontDestroyOnLoad(Instance);
        } 
        else Destroy(gameObject);
    }

    /// <summary>
    /// ��巹���� Ű �� ��ȯ
    /// </summary>
    /// <param name="argIndex">�ε���</param>
    /// <returns>Ű��</returns>
    public string GetAdressablePath(int argIndex)
    {
        return string.Format(m_format,argIndex);
    }

    /// <summary>
    /// ���� �� ��ȯ
    /// </summary>
    public void LoadGameChangeScene(SceneType.TYPE argSceneType)
    {
        StartCoroutine(ChangeScene(argSceneType));
    }

    /// <summary>
    /// �� ��ȯ �ڷ�ƾ
    /// </summary>
    /// <param name="argSceneType">�� Ÿ��</param>
    /// <returns>null</returns>
    IEnumerator ChangeScene(SceneType.TYPE argSceneType)
    {
        IsUIManager.m_paddingImg.gameObject.SetActive(true);
        IsUIManager.m_mainBack.SetActive(false);

        InitializeAndSaveBoolArray(IsTotalIndex);

        LoadBoolArr();

        string _scenename = GetSceneNameByGameType(argSceneType);

        IsSceneType = argSceneType;

        yield return new WaitForSeconds(0.5f);

        AsyncOperation _handle = SceneManager.LoadSceneAsync(_scenename);

        StartCoroutine(m_itemWidow.SettingAllSkin());

        while (!m_itemWidow.IsSkinFlag)
        {
            yield return null;
        }

        IschangeSceneFlag = true;

        while (!_handle.isDone)
        {
            yield return null;
        }

        Isuser = gameObject.AddComponent<User>();

        StartCoroutine(Isuser.Setting());
        
        while (!GManager.Instance.IsSettingFlag) yield return null;

        if (argSceneType == SceneType.TYPE.Main)
        {
            StartCoroutine(CheckPlayerSkinScore());
            IsUIManager.HandleSpriteRenderList();
            m_navigation.m_navMenuList[0].SetActive(true);
            IsUIManager.m_mainBack.SetActive(true);
        }

        m_navigation.m_navMenuList[0].SetActive(false);
        IschangeSceneFlag = false;
        IsUIManager.m_paddingImg.gameObject.SetActive(false);

        m_soundManager.PlayBGM(argSceneType);
    }


    /// <summary>
    /// �� Ÿ�� ��Ʈ�� ��ȯ
    /// </summary>
    /// <param name="argSceneType">�� Ÿ��</param>
    /// <returns>�� Ÿ�� 2 ��Ʈ��</returns>
    private string GetSceneNameByGameType(SceneType.TYPE argSceneType)
    {
        switch (argSceneType)
        {
            case SceneType.TYPE.Wave:
                return "Wave";
            case SceneType.TYPE.WallToWall:
                return "WallToWall";
            case SceneType.TYPE.LeftOrRight:
                return "LeftOrRight";
            case SceneType.TYPE.JumpAndShoot:
                return "JumpAndShoot";
            case SceneType.TYPE.ColorJump:
                return "ColorJump";
            case SceneType.TYPE.Main:
                m_navigation.OnNavBtnClick(NavMenuType.TYPE.Home);
                return "Main";
            default:
                return null;
        }
    }

    /// <summary>
    /// �� ��ȯ
    /// 1�� : �κ��
    /// 2�� : ���� ����
    /// </summary>
    /// <param name="argIndex">�ε���</param>
    public void ChangeScene(int argIndex)
    {
        switch (argIndex)
        {
            case 1:
                StartCoroutine(ChangeScene(SceneType.TYPE.Main));
                break;
        }
    }

    /// <summary>
    /// ���� ������ ��ȯ
    /// ���� �ϳ��� �� �ϰ� �;����� �ٸ� ��ũ��Ʈ�� �̻��ϰ� �����־
    /// �̰ɷ� ��ü
    /// �� �ּ��� ���ô� ����� �� �� ���� �̴ϴ� �Ƹ���..?
    /// </summary>
    public void GotoMain()
    {
        IsUIManager.m_paddingImg.gameObject.SetActive(true);
        SceneManager.LoadScene("Main");
        m_navigation.OnNavBtnClick(NavMenuType.TYPE.Home);
        Invoke("Padiingimghandle",0.03f);
        GManager.Instance.IsSceneType = SceneType.TYPE.Main;
    }

    /// <summary>
    /// ¥ġ����...�׷��� ��...
    /// </summary>
    public void Padiingimghandle()
    {
        IsUIManager.m_paddingImg.gameObject.SetActive(false);
    }


    /// <summary>
    /// ���� ���� �� �ش� ������ ���ھ ����
    /// </summary>
    /// <param name="argGameType">�ش� ������ Ÿ��</param>
    /// <param name="argScore">�ش� ������ ������</param>
    public void AddScoreInDic(SceneType.TYPE argSceneType, int argScore)
    {
        IsPrefSaveFlag = false;

        try
        {
            int _eScore = PlayerPrefs.GetInt($"Stage_{argSceneType.ToString()}_Score",0);
            int _newScore = _eScore + argScore;
            PlayerPrefs.SetInt($"Stage_{argSceneType.ToString()}_Score", _newScore);

            int _totalScore = 0;
            foreach (SceneType.TYPE gameType in System.Enum.GetValues(typeof(SceneType.TYPE)))
            {
                _totalScore += PlayerPrefs.GetInt($"Stage_{gameType.ToString()}_Score", 0);
            }
            PlayerPrefs.SetInt("Total_Score", _totalScore);

            PlayerPrefs.Save();
        }

        catch
        {
            Debug.Log("���� ����");
        }
    }

    /// <summary>
    /// �÷��̾� ��Ų �迭 �ֱ�
    /// </summary>
    /// <returns></returns>
    public IEnumerator CheckPlayerSkinScore()
    {
        int _curIndex = 0;

        int _totalUserIndex = GManager.Instance.IsTotalIndex;

        m_playerSkinArr = new int[IsTotalIndex];

        if (m_openScoreArr == null || m_openScoreArr.Length != IsTotalIndex)
        {
            m_openScoreArr = new int[IsTotalIndex];
        }


        int[] arr = GManager.Instance.m_openScoreArr;

        while (_curIndex < _totalUserIndex)
        {
            m_playerSkinArr[_curIndex] = _curIndex;

            string _path = GManager.Instance.GetAdressablePath(_curIndex);

            AsyncOperationHandle _handle = Addressables.LoadAssetAsync<UserData>(_path);

            yield return _handle;

            if (_handle.Result != null)
            {
                UserData _intScore = _handle.Result as UserData;

                if (_intScore != null)
                {
                    arr[_curIndex] = _intScore.IsOpenScore;
                }

                Addressables.Release(_handle);

                _curIndex++;
            }
        }

        GManager.Instance.m_openScoreArr = arr;

        ChangeUnLock();
    }

    /// <summary>
    /// ���� ���� �� �ʱ�ȭ �� ���� 
    /// </summary>
    /// <param name="argsize"></param>
    void InitializeAndSaveBoolArray(int argsize)
    {
        if (!PlayerPrefs.HasKey("PlayerSkinFlags"))
        {
            bool[] boolArray = new bool[argsize];
            boolArray[0] = true;
            for (int i = 1; i < argsize; i++)
            {
                boolArray[i] = false;
            }

            SaveBoolArr(boolArray);
        }
    }

    /// <summary>
    /// bool�� �迭 ����
    /// </summary>
    /// <param name="boolArray"></param>
    public void SaveBoolArr(bool[] argboolArr)
    {
        string _boolString = string.Join(",", System.Array.ConvertAll(argboolArr, b => b.ToString()));
        PlayerPrefs.SetString("PlayerSkinFlags", _boolString);

        PlayerPrefs.Save();
    }

    public bool[] LoadBoolArr()
    {
        if (PlayerPrefs.HasKey("PlayerSkinFlags"))
        {
            string _boolString = PlayerPrefs.GetString("PlayerSkinFlags");

            string[] _boolParts = _boolString.Split(',');
            return System.Array.ConvertAll(_boolParts, bool.Parse);
        }
        else
        {
            InitializeAndSaveBoolArray(IsTotalIndex);
            string _boolString = PlayerPrefs.GetString("PlayerSkinFlags");
            string[] _boolParts = _boolString.Split(',');
            return System.Array.ConvertAll(_boolParts, bool.Parse);
        }
    }

    public void ChangeUnLock()
    {
        int _total = PlayerPrefs.GetInt("Total_Score");

        int _i = 0;

        IsplayerSkinFlagArr = new bool[IsTotalIndex];

        while (_i<m_openScoreArr.Length)
        {
            if (m_openScoreArr[_i] > _total)
            {
                IsplayerSkinFlagArr[_i] = false;
            }

            else
            {
                IsplayerSkinFlagArr[_i] = true;
            }
            _i++;
        }

        SaveBoolArr(IsplayerSkinFlagArr);

        m_itemWidow.SetSkinLock();
    }
}