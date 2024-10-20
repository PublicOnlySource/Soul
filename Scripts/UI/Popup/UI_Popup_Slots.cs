using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup_Slots : UI_Popup
{
    [SerializeField]
    List<UI_DataSlot> slots = new List<UI_DataSlot>();
    [SerializeField]
    private Button hideButton;

    public override void Init()
    {
        base.Init();
        AddButtonEvent();
        UpdateSlotText();
    }

    private void AddButtonEvent()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].Init(this, i);
        }

        hideButton.onClick.AddListener(() =>
        {
            PopupManager.Instance.Hide<UI_Popup_Slots>();
        });
        UtilManager.Instance.AddButtonEnterSound(hideButton.gameObject);
    }

    private void UpdateSlotText()
    {
        Dictionary<string, SaveData> datas = DataManager.Instance.Datas;
        string fileName = string.Empty;
        string bonfireTitle = string.Empty;

        for (int i = 0; i < slots.Count; i++)
        {
            fileName = DataManager.Instance.GetFileName(i);
            if (datas.TryGetValue(fileName, out SaveData data))
            {
                bonfireTitle = DataManager.Instance.GetString(data.GetLastBonfireTitleIndex());
                slots[i].SetLastSaveAreaText(bonfireTitle);

                TimeSpan time = TimeSpan.FromSeconds((double)data.PlayTime);
                slots[i].SetPlayTimeText(time.ToString(@"hh\:mm\:ss"));
            }
        }
    }

    public void StartGame(int index)
    {
        SoundManager.Instance.StopBGMWithFadeOut(() =>
        {
            DataManager.Instance.LoadGame(index);
            SceneLoader.Instance.LoadScene(Enums.Scene.Title, Enums.Scene.Main);
        });
    }
}
