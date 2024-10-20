using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup_Game : UI_Popup
{
    [SerializeField]
    private Button inventoryButton;
    [SerializeField]
    private Button settingButton;
    [SerializeField]
    private Button gameExitButton;


    public override void Init()
    {
        base.Init();
        AddButtonEvent();
    }

    private void AddButtonEvent()
    {
        inventoryButton.onClick.AddListener(() =>
        {
            PopupManager.Instance.Show<UI_Popup_Inventory>();
        });

        settingButton.onClick.AddListener(() =>
        {
            PopupManager.Instance.Show<UI_Popup_Setting>();
        });

        gameExitButton.onClick.AddListener(() =>
        {
            PopupManager.Instance.Show<UI_Popup_Exit>();
        });

        UtilManager.Instance.AddButtonEnterSound(inventoryButton.gameObject);
        UtilManager.Instance.AddButtonEnterSound(settingButton.gameObject);
        UtilManager.Instance.AddButtonEnterSound(gameExitButton.gameObject);
    }
}
