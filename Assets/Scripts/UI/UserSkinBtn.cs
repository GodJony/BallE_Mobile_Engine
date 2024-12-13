using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSkinBtn : MonoBehaviour
{
    /// <summary>
    /// ���� ��Ų �ε���
    /// </summary>
    private int m_userSkinIndex = 0;

    MyUIManager m_uiManger;

    /// <summary>
    /// ��ư Ŭ�� �� ��������Ʈ ü����
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
