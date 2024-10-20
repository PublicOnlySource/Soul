using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WindowToggle : MonoBehaviour
{
    [SerializeField]
    private FullScreenMode type;

    private Toggle toggle;
    private Action<FullScreenMode> changeCallback;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();

        string currentType = PlayerPrefs.GetString(Constants.Game.STRING_WINDOWSETTING, FullScreenMode.ExclusiveFullScreen.ToString());
        toggle.isOn = type.ToString().Equals(currentType);

        toggle.onValueChanged.AddListener((isChecked) => UpdateState(isChecked));
    }

    private void UpdateState(bool isChecked)
    {
        if (!isChecked)
            return;

        changeCallback?.Invoke(type);
    }

    public void SetChangeCallback(Action<FullScreenMode> callback)
    {
        changeCallback = callback;
    }
}
