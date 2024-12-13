using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="UserData", menuName ="Jony/Creat User Data",order =1)]
public class UserData : ScriptableObject
{
    /// <summary>
    /// �̸�
    /// </summary>
    [SerializeField] private string m_name = string.Empty;

    /// <summary>
    /// ����
    /// </summary>
    [SerializeField] private string m_contents = string.Empty;

    /// <summary>
    /// �ε��� ��ȣ
    /// </summary>
    [SerializeField] private int m_index = -1;

    /// <summary>
    /// ��������Ʈ
    /// </summary>
    [SerializeField] private Sprite m_sprite = null;

    /// <summary>
    /// �ش� ��ü�� �ر� ���ھ�
    /// </summary>
    [SerializeField] private int m_openScore = -1;


    /// <summary>
    /// �̸� ��ȯ
    /// </summary>
    public string IsName { get { return m_name; } }

    /// <summary>
    /// ���� ��ȯ
    /// </summary>
    public string IsContents { get { return m_contents; } }

    /// <summary>
    /// �ε��� ��ȯ
    /// </summary>
    public int IsIndex { get { return m_index; } }


    /// <summary>
    /// ��������Ʈ ��ȯ
    /// </summary>
    public Sprite IsSprite { get { return m_sprite; } }

    /// <summary>
    /// ���� ���ھ� ��ȯ
    /// </summary>
    public int IsOpenScore { get { return m_openScore; } }
}
