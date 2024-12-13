using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    /// <summary>
    /// off 하단 네비게이션 메뉴
    /// 0번 : 스킨, 1번 : 홈, 2번 : 세팅
    /// </summary>
    [SerializeField] private List<GameObject> m_navOnObjList;

    /// <summary>
    /// on 하단 네비게이션 메뉴
    /// 0번 : 스킨, 1번 : 홈, 2번 : 세팅
    /// </summary>
    [SerializeField] private List<GameObject> m_navOffObjList;

    /// <summary>
    /// 메뉴 오브젝트
    /// </summary>
    public List<GameObject> m_navMenuList;

    /// <summary>
    /// 네비게이션 버튼 클릭 시 해당 메뉴로 이동
    /// </summary>
    /// <param name="argNavMenuType">네비게이션 메뉴 타입</param>
    public void OnNavBtnClick(NavMenuType.TYPE argNavMenuType)
    {
        switch (argNavMenuType)
        {
            case NavMenuType.TYPE.Skin:
                GManager.Instance.IsUIManager.ExixPopUp();
                m_navMenuList[0].SetActive(true);
                m_navMenuList[1].SetActive(false);
                m_navMenuList[2].SetActive(false);

                m_navOnObjList[0].SetActive(true);
                m_navOnObjList[1].SetActive(false);
                m_navOnObjList[2].SetActive(false);

                m_navOffObjList[0].SetActive(false);
                m_navOffObjList[1].SetActive(true);
                m_navOffObjList[2].SetActive(true);
                break;
            case NavMenuType.TYPE.Home:
                GManager.Instance.IsUIManager.ExixPopUp();

                m_navMenuList[0].SetActive(false);
                m_navMenuList[1].SetActive(true);
                m_navMenuList[2].SetActive(false);

                m_navOnObjList[0].SetActive(false);
                m_navOnObjList[1].SetActive(true);
                m_navOnObjList[2].SetActive(false);

                m_navOffObjList[0].SetActive(true);
                m_navOffObjList[1].SetActive(false);
                m_navOffObjList[2].SetActive(true);
                break;
            case NavMenuType.TYPE.Setting:
                GManager.Instance.IsUIManager.ExixPopUp();

                m_navMenuList[0].SetActive(false);
                m_navMenuList[1].SetActive(false);
                m_navMenuList[2].SetActive(true);

                m_navOnObjList[0].SetActive(false);
                m_navOnObjList[1].SetActive(false);
                m_navOnObjList[2].SetActive(true);

                m_navOffObjList[0].SetActive(true);
                m_navOffObjList[1].SetActive(true);
                m_navOffObjList[2].SetActive(false);
                break;
        }
    }
}
