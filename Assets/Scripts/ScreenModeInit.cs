using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenModeInit : MonoBehaviour
{
    private void Awake()
    {
        bool isOnBool = false;
        int isOnInt = PlayerPrefs.GetInt("FullScreen", 0);
        if (isOnInt == 1)
        {
            isOnBool = true;
        }
        GetComponent<Toggle>().isOn = isOnBool;
    }

    public void SetFullScreen(bool isFullScreen)
    {
        int isFS = 0;
        if (isFullScreen)
        {
            isFS = 1;
        }
        Debug.Log("FullScreen: " + isFullScreen);
        PlayerPrefs.SetInt("FullScreen", isFS);
        Screen.fullScreen = isFullScreen;
    }
}
