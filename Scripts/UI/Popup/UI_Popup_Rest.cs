using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup_Rest : UI_Popup
{
    [SerializeField]
    private Button upgradeButton;
    [SerializeField]
    private Button exitButton;

    public override void Init()
    {
        base.Init();
        exitButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.RemoveAllListeners();

        upgradeButton.onClick.AddListener(() => ShowUpgrade());
        exitButton.onClick.AddListener(() => ExitRest());

        UtilManager.Instance.AddButtonEnterSound(upgradeButton.gameObject);
        UtilManager.Instance.AddButtonEnterSound(exitButton.gameObject);
    }

    private void ShowUpgrade()
    {
        PopupManager.Instance.Show<UI_Popup_Upgrade>();
        PopupManager.Instance.Hide<UI_Popup_Rest>();
    }

    private void ExitRest()
    {
        EventManager.Instance.TriggerEvent(Enums.EventType.Player_void_restExit);
    }
}
