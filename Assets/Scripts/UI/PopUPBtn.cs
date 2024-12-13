using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUPBtn : MonoBehaviour
{
    /// <summary>
    /// ÆË¾÷
    /// </summary>
    [SerializeField] GameObject m_popUP;

    public void OnPopUPBtn()
    {
        GManager.Instance.m_soundManager.UITOuch();

        GManager.Instance.IsUIManager.m_namePopUp = gameObject.name;

        m_popUP.SetActive(true);
    }

    public void ExitBtn()
    {
        GManager.Instance.m_soundManager.UITOuch();

        GManager.Instance.IsUIManager.m_namePopUp = string.Empty;

        m_popUP.SetActive(false);
    } 
}
