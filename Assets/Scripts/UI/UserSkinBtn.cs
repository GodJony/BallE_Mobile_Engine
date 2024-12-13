using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSkinBtn : MonoBehaviour
{
    /// <summary>
    /// 유저 스킨 인덱스
    /// </summary>
    private int m_userSkinIndex = 0;

    MyUIManager m_uiManger;

    /// <summary>
    /// 버튼 클릭 시 스프라이트 체인지
    /// </summary>
    public void OnBtnChange()
    {
        GManager.Instance.m_soundManager.UITOuch();


        m_uiManger = GManager.Instance.IsUIManager;

        m_userSkinIndex = int.Parse(gameObject.name);

        if (GManager.Instance.IsplayerSkinFlagArr[m_userSkinIndex])
        {
            PlayerPrefs.SetInt("Index", m_userSkinIndex);

            m_uiManger.ChangePlayerSprite(m_userSkinIndex);
        }
    }
}
