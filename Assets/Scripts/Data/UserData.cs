using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="UserData", menuName ="Jony/Creat User Data",order =1)]
public class UserData : ScriptableObject
{
    /// <summary>
    /// 이름
    /// </summary>
    [SerializeField] private string m_name = string.Empty;

    /// <summary>
    /// 설명
    /// </summary>
    [SerializeField] private string m_contents = string.Empty;

    /// <summary>
    /// 인덱스 번호
    /// </summary>
    [SerializeField] private int m_index = -1;

    /// <summary>
    /// 스프라이트
    /// </summary>
    [SerializeField] private Sprite m_sprite = null;

    /// <summary>
    /// 해당 객체의 해금 스코어
    /// </summary>
    [SerializeField] private int m_openScore = -1;


    /// <summary>
    /// 이름 반환
    /// </summary>
    public string IsName { get { return m_name; } }

    /// <summary>
    /// 설명 반환
    /// </summary>
    public string IsContents { get { return m_contents; } }

    /// <summary>
    /// 인덱스 반환
    /// </summary>
    public int IsIndex { get { return m_index; } }


    /// <summary>
    /// 스프라이트 반환
    /// </summary>
    public Sprite IsSprite { get { return m_sprite; } }

    /// <summary>
    /// 오픈 스코어 반환
    /// </summary>
    public int IsOpenScore { get { return m_openScore; } }
}
