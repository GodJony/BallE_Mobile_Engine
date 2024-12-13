using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingBtn : MonoBehaviour
{
    /// <summary>
    /// 0 : off 1: on
    /// </summary>
    [SerializeField] int index = -1;

    /// <summary>
    /// ���� Ÿ��
    /// </summary>
    [SerializeField] SoundType.TYPE m_soundType = SoundType.TYPE.SFX;


    [SerializeField] private GameObject m_TargrtObj;

    public void OnVBtnClick()
    {
        switch (index)
        {
            case 0:
                m_TargrtObj.SetActive(true);
                gameObject.SetActive(false);
                break;
            case 1:
                m_TargrtObj.SetActive(true);
                gameObject.SetActive(false);

                switch (m_soundType)
                {
                    case SoundType.TYPE.BGM:
                        GManager.Instance.m_soundManager.MuteBgm();
                        break;
                    case SoundType.TYPE.SFX:
                        GManager.Instance.m_soundManager.MuteSFX();
                        break;
                }


                break;
        }
    }
}