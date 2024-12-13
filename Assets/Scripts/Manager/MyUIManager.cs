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
    /// ���� ���� ������Ʈ
    /// </summary>
    [SerializeField] private GameObject m_gameoverObj = null;

    /// <summary>
    /// Ÿ��Ʋ UI ������Ʈ
    /// </summary>
    [SerializeField] private GameObject m_titleUIObj = null;

    /// <summary>
    /// ���� �� ��׶��� ������Ʈ
    /// </summary>
    public GameObject m_mainBack;

    /// <summary>
    /// ���� ���� ��ư ������Ʈ
    /// </summary>
    [SerializeField] private GameObject m_gameSelectBtn = null;

    /// <summary>
    /// �� ��ȯ �� �ε� �̹���
    /// </summary>
    public Image m_paddingImg = null;

    /// <summary>
    /// Main������ �������� ��Ż ���ھ� �ؽ�Ʈ
    /// Main Scene�̿� ��� ���ϴ� �ǵ��� ���ڱ�
    /// </summary>
    [SerializeField] private TextMeshProUGUI m_totalScoreTXT;

    /// <summary>
    /// ���� ���� �� ��Ÿ���� ���� ���ھ� �ؽ�Ʈ
    /// </summary>
    [SerializeField] private TextMeshProUGUI m_scoreText;

    /// <summary>
    /// ���� ���� �� ��Ÿ���� ����Ʈ ���ھ� �ؽ�Ʈ
    /// </summary>
    [SerializeField] private TextMeshProUGUI m_bestScoreText;

    /// <summary>
    /// 0�� : Ȩ ȭ�� ĳ���� ������ / 1�� : ��Ų�� ��������Ʈ ������ ����Ʈ
    /// </summary>
    [SerializeField] private List<Image> m_playerSpriteRenderList;

    /// <summary>
    /// ��Ż ���ھ� ���α׷���
    /// </summary>
    [SerializeField] private Slider m_totalSlider;

    /// <summary>
    /// ���α׷��� �� ������
    /// </summary>
    [SerializeField] List<Image> m_progressRenderList;

    /// <summary>
    /// �ر� �� ǥ�õǴ� ��������Ʈ
    /// </summary>
    [SerializeField] Sprite m_checkSprite;

    /// <summary>
    /// ���� �ر� ���� ��������Ʈ
    /// </summary>
    [SerializeField] Sprite m_nowSkinSprite;

     private void Update()
     {
        GameOver();

        //���� ����
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
    /// ��Ż ���ھ ���� ���α׷��� ��
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
    /// ���Ӿ� ��ȯ
    /// </summary>
    /// <param name="argGameType">���� Ÿ��</param>
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
    /// ���� ������ �޼���
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
    /// ���÷��� ��ư Ŭ��
    /// </summary>
    public void RetryBtnOnClick()
    {
        StartCoroutine(RetryBtnCorutine());
    }

    /// <summary>
    /// ���÷��� ��ư Ŭ�� �� �ڷ�ƾ ����
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
