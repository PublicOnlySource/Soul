using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup_Setting : UI_Popup
{
    [SerializeField]
    private UI_Setting_Graphic graphic;
    [SerializeField]
    private UI_Setting_Sound sound;
    [SerializeField]
    private Button graphicButton;
    [SerializeField]
    private Button soundButton;
    [SerializeField]
    private Button hideButton;

    private void OnEnable()
    {
        graphic.Show();
    }

    private void OnDisable()
    {
        AllTabHide();
    }

    public override void Init()
    {
        base.Init();

        graphic.Init();
        sound.Init();

        AddButtonEvnet();
    }

    private void AddButtonEvnet()
    {
        graphicButton.onClick.AddListener(() =>
        {
            AllTabHide();
            graphic.Show();
        });

        soundButton.onClick.AddListener(() =>
        {
            AllTabHide();
            sound.Show();
        });

        hideButton.onClick.AddListener(() =>
        {
            PopupManager.Instance.Hide<UI_Popup_Setting>();
        });

        UtilManager.Instance.AddButtonEnterSound(graphicButton.gameObject);
        UtilManager.Instance.AddButtonEnterSound(soundButton.gameObject);
        UtilManager.Instance.AddButtonEnterSound(hideButton.gameObject);
    }

    private void AllTabHide()
    {
        graphic.Hide();
        sound.Hide();
    }
}
