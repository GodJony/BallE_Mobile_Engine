using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoadBtn : MonoBehaviour
{
    [SerializeField] SceneType.TYPE m_SceneType;

    [SerializeField] MyUIManager m_UIManager;

    [SerializeField] GameObject m_popUp;

    /// <summary>
    /// 씬 전환 버튼 클릭
    /// </summary>
    public void OnBtnClick()
    {
        GManager.Instance.m_soundManager.UITOuch();
        m_UIManager.ChangeGameBtnClick(m_SceneType);
        m_popUp.SetActive(false);
    }
}
