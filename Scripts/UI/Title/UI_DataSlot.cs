using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class UI_DataSlot : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private TMP_Text lastSaveAreaText;
    [SerializeField]
    private TMP_Text playTimeText;

    private UI_Popup_Slots slots;

    public void Init(UI_Popup_Slots slots, int index)
    {
        this.slots = slots;
        lastSaveAreaText.text = string.Empty;
        playTimeText.text = string.Empty;
        button.onClick.AddListener(() => slots.StartGame(index));

        UtilManager.Instance.AddButtonEnterSound(gameObject);
    }

    public void SetLastSaveAreaText(string text)
    {
        lastSaveAreaText.text = text;
    }

    public void SetPlayTimeText(string text)
    {
        playTimeText.text = text;
    }
}
