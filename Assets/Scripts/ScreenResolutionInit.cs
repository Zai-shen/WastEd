using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenResolutionInit : MonoBehaviour
{
    private Resolution[] resolutions;
    private TMP_Dropdown dropDown;

    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        dropDown = GetComponent<TMP_Dropdown>();
        dropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + " x " + resolutions[i].height);

            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        dropDown.AddOptions(options);
        dropDown.value = currentResolutionIndex;
        dropDown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        Debug.Log("Changed resolution to: " + res);
    }

}
