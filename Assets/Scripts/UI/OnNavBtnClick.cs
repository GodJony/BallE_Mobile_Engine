using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnNavBtnClick : MonoBehaviour
{
    public NavMenuType.TYPE m_navMenuType;

    Navigation m_nav;

    private void Start()
    {
        m_nav =FindObjectOfType<Navigation>();
    }

    /// <summary>
    /// 버튼 클릭 시
    /// </summary>
    public void OnBtnClick()
    {
        m_nav.OnNavBtnClick(m_navMenuType);
    }
}
