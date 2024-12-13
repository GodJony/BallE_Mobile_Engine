using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIAni : MonoBehaviour
{
    /// <summary>
    /// 터치 투 스타트 텍스트 애니메이션
    /// </summary>
    [SerializeField] private TextMeshProUGUI m_ttStart = null;

    private float m_deltaAlpha = 2f;

    private void Update()
    {
        float _alpha = Mathf.PingPong(Time.time / m_deltaAlpha, 0.3f);

        SetAlpha(_alpha);
    }

    /// <summary>
    /// 텍스트 알파값 조절
    /// </summary>
    /// <param name="argAlpha">알파값</param>
    private void SetAlpha(float argAlpha)
    {
        Color _curColor = m_ttStart.color;
        _curColor.a = argAlpha;
        m_ttStart .color = _curColor;
    }
}
