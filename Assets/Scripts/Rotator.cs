﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 rotationPerFrame;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(rotationPerFrame * Time.deltaTime);
    }
}
