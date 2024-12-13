using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MyUIManager : MonoBehaviour
{
    /// <summary>
    /// 게임 오버 오브젝트
    /// </summary>
    [SerializeField] private GameObject m_gameoverObj = null;

    /// <summary>
    /// 타이틀 UI 오브젝트
    /// </summary>
    [SerializeField] private GameObject m_titleUIObj = null;

    /// <summary>
    /// 메인 씬 백그라운드 오브젝트
    /// </summary>
    public GameObject m_mainBack;

    /// <summary>
    /// 게임 선택 버튼 오브젝트
    /// </summary>
    [SerializeField] private GameObject m_gameSelectBtn = null;

    /// <summary>
    /// 씬 전환 시 로딩 이미지
    /// </summary>
    public Image m_paddingImg = null;

    /// <summary>
    /// Main씬에서 보여지는 토탈 스코어 텍스트
    /// Main Scene이외 사용 안하니 건들지 말자구
    /// </summary>
    [SerializeField] private TextMeshProUGUI m_totalScoreTXT;

    /// <summary>
    /// 게임 오버 시 나타나는 최종 스코어 텍스트
    /// </summary>
    [SerializeField] private TextMeshProUGUI m_scoreText;

    /// <summary>
    /// 게임 오버 시 나타나는 베스트 스코어 텍스트
    /// </summary>
    [SerializeField] private TextMeshProUGUI m_bestScoreText;

    /// <summary>
    /// 0번 : 홈 화면 캐릭터 랜더러 / 1번 : 스킨의 스프라이트 랜더러 리스트
    /// </summary>
    [SerializeField] private List<Image> m_playerSpriteRenderList;

    /// <summary>
    /// 토탈 스코어 프로그래스
    /// </summary>
    [SerializeField] private Slider m_totalSlider;

    /// <summary>
    /// 프로그래스 바 랜더러
    /// </summary>
    [SerializeField] List<Image> m_progressRenderList;

    /// <summary>
    /// 해금 시 표시되는 스프라이트
    /// </summary>
    [SerializeField] Sprite m_checkSprite;

    /// <summary>
    /// 현재 해금 중인 스프라이트
    /// </summary>
    [SerializeField] Sprite m_nowSkinSprite;

     private void Update()
     {
        GameOver();

        //상태 패턴
        switch (GManager.Instance.IsSceneType)
        {
            case SceneType.TYPE.Title:
                GManager.Instance.m_NavAndMenu[0].SetActive(false);
                GManager.Instance.m_NavAndMenu[1].SetActive(false);
                m_titleUIObj.SetActive(true);
                m_gameSelectBtn.SetActive(false);
                MoveFromTitleToMain();
                break;
            case SceneType.TYPE.Main:
                if (m_totalScoreTXT != null) m_totalScoreTXT.text = PlayerPrefs.GetInt("Total_Score", 0).ToString();
                GManager.Instance.m_NavAndMenu[0].SetActive(true);
                GManager.Instance.m_NavAndMenu[1].SetActive(true);
                m_titleUIObj.SetActive(false);
                m_gameSelectBtn.SetActive(true);
                ProgressBar();
                break;
            default:
                GManager.Instance.m_NavAndMenu[0].SetActive(false);
                GManager.Instance.m_NavAndMenu[1].SetActive(false);
                m_gameSelectBtn.SetActive(false);
                break;
        }
    }

    public void MoveFromTitleToMain()
    {
#if UNITY_EDITOR || UNITY_STANDALONE

        if (Input.GetMouseButtonDown(0))
        {
            GManager.Instance.ChangeScene(1);

        }
#endif

        if (Input.touchCount > 0)
        {
            Touch _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Began)         
            {
                GManager.Instance.m_soundManager.UITOuch();

                GManager.Instance.ChangeScene(1);
            }
        }

        if (m_totalScoreTXT != null) m_totalScoreTXT.text = PlayerPrefs.GetInt("Total_Score", 0).ToString();
    }

    /// <summary>
    /// 토탈 스코어에 따른 프로그래스 바
    /// </summary>

    public void ProgressBar()
    {
        m_totalSlider.value = PlayerPrefs.GetInt("Total_Score") / 1000;
    }

    public void HandleSpriteRenderList()
    {
        bool[] _flag = GManager.Instance.LoadBoolArr();

        for(int i=0; i<_flag.Length;i++)
        {
            switch (_flag[i])
            {
                case true:
                    m_progressRenderList[i].sprite = m_checkSprite;
                    break;
                case false:
                    m_nowSkinSprite = GManager.Instance.m_itemWidow.IsallSkins[i].IsSprite;
                    m_progressRenderList[i].sprite = m_nowSkinSprite;
                    break;
            }
        }
    }

    /// <summary>
    /// 게임씬 전환
    /// </summary>
    /// <param name="argGameType">게임 타입</param>
    public void ChangeGameBtnClick(SceneType.TYPE argSceneType)
    {
        GManager.Instance.LoadGameChangeScene(argSceneType);
    }

    public void GotoMain()
    {
        GManager.Instance.LoadGameChangeScene(SceneType.TYPE.Main);
        GManager.Instance.IsGameOverFlag = false;
    }

    /// <summary>
    /// 게임 오버시 메서드
    /// </summary>
    public void GameOver()
    {
        m_gameoverObj.SetActive(GManager.Instance.IsGameOverFlag);
        if (GManager.Instance.IsGameOverFlag)
        {
            m_bestScoreText.text = PlayerPrefs.GetInt($"Stage_{GManager.Instance.IsSceneType}_Best_Score").ToString();
        }
    }

    /// <summary>
    /// 리플레이 버튼 클릭
    /// </summary>
    public void RetryBtnOnClick()
    {
        StartCoroutine(RetryBtnCorutine());
    }

    /// <summary>
    /// 리플레이 버튼 클릭 시 코루틴 실행
    /// </summary>
    /// <returns></returns>
    IEnumerator RetryBtnCorutine()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartCoroutine(GManager.Instance.Isuser.Setting());
        while (!GManager.Instance.IsSettingFlag) yield return null;

        GManager.Instance.IsGameOverFlag = false;

    }

    public void ChangePlayerSprite(int argIndex)
    {
        m_playerSpriteRenderList[0].sprite = GManager.Instance.m_itemWidow.ReturnSprite(argIndex);
        m_playerSpriteRenderList[1].sprite = GManager.Instance.m_itemWidow.ReturnSprite(argIndex);
        m_playerSpriteRenderList[2].sprite = GManager.Instance.m_itemWidow.ReturnSprite(argIndex);
    }
}
