using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTriangleParticleRotator : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotation = default;

    void Update()
    {
        if (Time.timeScale < 0.01f)
        {
            transform.Rotate(rotation * 0.1f * Time.unscaledDeltaTime);
        }
        else
        {
            transform.Rotate(rotation * Time.deltaTime);
        }
    }
}
