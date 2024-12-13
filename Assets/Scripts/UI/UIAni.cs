using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIAni : MonoBehaviour
{
    /// <summary>
    /// ��ġ �� ��ŸƮ �ؽ�Ʈ �ִϸ��̼�
    /// </summary>
    [SerializeField] private TextMeshProUGUI m_ttStart = null;

    private float m_deltaAlpha = 2f;

    private void Update()
    {
        float _alpha = Mathf.PingPong(Time.time / m_deltaAlpha, 0.3f);

        SetAlpha(_alpha);
    }

    /// <summary>
    /// �ؽ�Ʈ ���İ� ����
    /// </summary>
    /// <param name="argAlpha">���İ�</param>
    private void SetAlpha(float argAlpha)
    {
        Color _curColor = m_ttStart.color;
        _curColor.a = argAlpha;
        m_ttStart .color = _curColor;
    }
}
