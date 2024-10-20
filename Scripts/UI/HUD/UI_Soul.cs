using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Soul : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    private void Awake()
    {
        EventManager.Instance.AddListener<int>(Enums.EventType.UI_int_updateSoulText, SetSoulText);
    }

    private void SetSoulText(int soul)
    {
        text.text = soul.ToString();
    }
}
