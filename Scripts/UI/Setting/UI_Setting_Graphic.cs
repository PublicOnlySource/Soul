using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Setting_Graphic : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;
    [SerializeField]
    private List<UI_WindowToggle> toggles;

    private List<Resolution> resolutions = new List<Resolution>();
    private int selectResolutionValue;
    private FullScreenMode fullScreenMode;

    private void SetResolutionList()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRateRatio.value == Constants.DEFAULT_FRAME)
            {
                if (Screen.resolutions[i].width < 1024)
                    continue;

                if (selectResolutionValue == -1 && Screen.resolutions[i].width == Screen.width && Screen.resolutions[i].height == Screen.height)
                {
                    selectResolutionValue = i;
                }

                resolutions.Add(Screen.resolutions[i]);
            }
        }
    }

    private void SetDropdown()
    {
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutions.ConvertAll(r => $"{r.width} x {r.height}"));
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(ChangeResolution);
        resolutionDropdown.value = selectResolutionValue;
    }

    private void ChangeResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, fullScreenMode, resolution.refreshRateRatio);
        Application.runInBackground = fullScreenMode != FullScreenMode.ExclusiveFullScreen;
        selectResolutionValue = index;
        
        PlayerPrefs.SetInt(Constants.Game.STRING_RESOLUTION, index);
        PlayerPrefs.SetString(Constants.Game.STRING_WINDOWSETTING, fullScreenMode.ToString());

        print($"{index}, {fullScreenMode}");
    }

    private void SetToggleCallback()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            toggles[i].SetChangeCallback((mode) =>
            {
                fullScreenMode = mode;
                ChangeResolution(selectResolutionValue);
            });
        }

    }

    public void Init()
    {
        selectResolutionValue = PlayerPrefs.GetInt(Constants.Game.STRING_RESOLUTION, -1);
        fullScreenMode = Enum.Parse<FullScreenMode>(PlayerPrefs.GetString(Constants.Game.STRING_WINDOWSETTING, FullScreenMode.ExclusiveFullScreen.ToString()));

        SetResolutionList();
        SetDropdown();       
        SetToggleCallback();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
