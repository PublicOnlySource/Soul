using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup_Exit : UI_Popup
{
    [SerializeField]
    private Button exitTitleButton;
    [SerializeField]
    private Button exitGameButton;
    [SerializeField]
    private Button hideButton;

    public override void Init()
    {
        base.Init();
        AddButtonEvent();
    }

    private void AddButtonEvent()
    {
        exitTitleButton.onClick.AddListener(() =>
        {
            Save();
           
            SoundManager.Instance.StopBGMWithFadeOut(() =>
            {
                InGameManager.Instance.Dispose();
                SceneLoader.Instance.LoadScene(Enums.Scene.Main, Enums.Scene.Title);
            });
        });

        exitGameButton.onClick.AddListener(() =>
        {
            Save();
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        });

        hideButton.onClick.AddListener(() =>
        {
            PopupManager.Instance.Hide<UI_Popup_Exit>();
        });

        UtilManager.Instance.AddButtonEnterSound(exitTitleButton.gameObject);
        UtilManager.Instance.AddButtonEnterSound(exitGameButton.gameObject);
        UtilManager.Instance.AddButtonEnterSound(hideButton.gameObject);
    }

    private void Save()
    {
        EventManager.Instance.TriggerEvent(Enums.EventType.Data_void_writeSaveData);
        DataManager.Instance.SaveGame();        
    }

}
