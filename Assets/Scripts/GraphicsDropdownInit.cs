using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GraphicsDropdownInit : MonoBehaviour
{
    private void Awake()
    {
        int quality = PlayerPrefs.GetInt("GraphicsQuality", 0);
        GetComponent<TMP_Dropdown>().value = quality;
        QualitySettings.SetQualityLevel(quality);
    }

    public void SetGraphicsQuality(int qualityInd)
    {
        PlayerPrefs.SetInt("GraphicsQuality", qualityInd);
        Debug.Log("Changed graphics quality to: " + qualityInd);
        QualitySettings.SetQualityLevel(qualityInd);
    }
}
