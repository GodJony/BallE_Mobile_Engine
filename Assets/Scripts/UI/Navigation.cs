using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    /// <summary>
    /// off �ϴ� �׺���̼� �޴�
    /// 0�� : ��Ų, 1�� : Ȩ, 2�� : ����
    /// </summary>
    [SerializeField] private List<GameObject> m_navOnObjList;

    /// <summary>
    /// on �ϴ� �׺���̼� �޴�
    /// 0�� : ��Ų, 1�� : Ȩ, 2�� : ����
    /// </summary>
    [SerializeField] private List<GameObject> m_navOffObjList;

    /// <summary>
    /// �޴� ������Ʈ
    /// </summary>
    public List<GameObject> m_navMenuList;

    /// <summary>
    /// �׺���̼� ��ư Ŭ�� �� �ش� �޴��� �̵�
    /// </summary>
    /// <param name="argNavMenuType">�׺���̼� �޴� Ÿ��</param>
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
