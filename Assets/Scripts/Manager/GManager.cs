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
    //당신 왜 코드를 뜯어보는 것이죠..? Hentai...키모...

    private string m_format = "Assets/Prefab/Data/UserData/{0:D}.asset";

    /// <summary>
    /// 유저
    /// </summary>
    public User Isuser { get; private set; }  = null;


    /// <summary>
    /// 플레이어 오브젝트
    /// </summary>
    public GameObject IsPlayerObj { get;  set; } = null;

    /// <summary>
    /// 전체 유저 스킨 데이터
    /// </summary>
    public int IsTotalIndex { get; private set; } = 10;

    /// <summary>
    /// 유저 인덱스
    /// </summary>
    public int m_userIndex = -1;

    public SoundManager m_soundManager;

    /// <summary>
    /// 세팅 플래그
    /// </summary>
    public bool IsSettingFlag = false;

    /// <summary>
    /// 모든 스테이지 토탈 스코어
    /// </summary>
    public int IsTotalScore { get; private set; } = 0;

    /// <summary>
    /// 현재 씬 타입
    /// </summary>
    public SceneType.TYPE IsSceneType { get; private set; } = SceneType.TYPE.Title;

    /// <summary>
    /// 씬 이동 플래그
    /// </summary>
    public bool IschangeSceneFlag { get; private set; } = false;

    /// <summary>
    /// 저장 플래그
    /// false : 저장 중, ttue : 저장 완료
    /// </summary>
    public bool IsPrefSaveFlag { get; private set; } = false;

    /// <summary>
    /// UI 메니저 싱글톤 구현
    /// </summary>
    public MyUIManager IsUIManager { get; private set; } = null;

    /// <summary>
    /// 게임 오버 플래그
    /// </summary>
    public bool IsGameOverFlag { get; set; } = false;

    public static GManager Instance { get; set; } = null;

    /// <summary>
    /// 네비게이션과 메뉴 오브젝트
    /// 1번 : 네비
    /// 2번 : 메뉴
    public List<GameObject> m_NavAndMenu;

    public Navigation m_navigation;

    public ItemWindow m_itemWidow;

    /// <summary>
    /// 해금 스코어 체크용
    /// </summary>
    private int[] m_openScoreArr;

    /// <summary>
    /// 플레이어 스킨 배열
    /// </summary>
    private int[] m_playerSkinArr;

    /// <summary>
    /// 플레이어가 해당 스킨을 해금했는지에 대한 배열
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
    /// 어드레서블 키 값 반환
    /// </summary>
    /// <param name="argIndex">인덱스</param>
    /// <returns>키값</returns>
    public string GetAdressablePath(int argIndex)
    {
        return string.Format(m_format,argIndex);
    }

    /// <summary>
    /// 게임 씬 전환
    /// </summary>
    public void LoadGameChangeScene(SceneType.TYPE argSceneType)
    {
        StartCoroutine(ChangeScene(argSceneType));
    }

    /// <summary>
    /// 씬 전환 코루틴
    /// </summary>
    /// <param name="argSceneType">씬 타입</param>
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
    /// 씬 타입 스트링 반환
    /// </summary>
    /// <param name="argSceneType">씬 타입</param>
    /// <returns>씬 타입 2 스트링</returns>
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
    /// 씬 변환
    /// 1번 : 로비로
    /// 2번 : 게임 씬으
    /// </summary>
    /// <param name="argIndex">인덱스</param>
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
    /// 메인 씬으로 전환
    /// 원래 하나로 다 하고 싶었지만 다른 스크립트가 이상하게 꼬여있어서
    /// 이걸로 대체
    /// 이 주석을 보시는 당신은 할 수 있을 겁니다 아마도..?
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
    /// 짜치지만...그래도 뭐...
    /// </summary>
    public void Padiingimghandle()
    {
        IsUIManager.m_paddingImg.gameObject.SetActive(false);
    }


    /// <summary>
    /// 게임 오버 후 해당 게임의 스코어를 넣음
    /// </summary>
    /// <param name="argGameType">해당 게임의 타입</param>
    /// <param name="argScore">해당 게임의 저엄수</param>
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
            Debug.Log("저장 실패");
        }
    }

    /// <summary>
    /// 플레이어 스킨 배열 넣기
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
    /// 최초 실행 시 초기화 후 저장 
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
    /// bool형 배열 저장
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