﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderInit : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("Volume") * 10;
    }

}
