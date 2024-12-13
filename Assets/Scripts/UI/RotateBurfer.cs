using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBurfer : MonoBehaviour
{
    [SerializeField] float m_rotSpeed;

    private void FixedUpdate()
    {
        Rotate();
    }
    void Rotate()
    {
        if (!GManager.Instance.IschangeSceneFlag) return;


        transform.Rotate(0f, 0f, m_rotSpeed * Time.deltaTime);
    }
}
