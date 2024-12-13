using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBurfer : MonoBehaviour
{
    [SerializeField] float m_rotSpeed;

    private void Update()
    {
        Rotate();
    }
    void Rotate()
    {
        transform.Rotate(0f, 0f, m_rotSpeed * Time.deltaTime);
    }
}
