using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScoreManager : MonoBehaviour
{
    public SceneType.TYPE m_SceneType = SceneType.TYPE.Wave;

    /// <summary>
    /// 이번 라운드에서 획득한 점수
    /// </summary>
    public int IsScore { get; set; } = 0;

}
