using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_StaminaBar : UI_BaseSliderBar
{
    private void Awake()
    {
        base.Init();
        EventManager.Instance.AddListener<float>(Enums.EventType.UI_float_updateStaminaBar, UpdateSliderBar);
        EventManager.Instance.AddListener<float>(Enums.EventType.UI_float_updateMaxStaminaBar, UpdateSliderBarMax);
    }
}
