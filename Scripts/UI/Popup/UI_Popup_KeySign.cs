using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Popup_KeySign : UI_Popup
{
    [SerializeField]
    private TMP_Text keyText;
    [SerializeField]
    private TMP_Text text;

    public void SetKeySign(string key, string text)
    {
        keyText.text = key;
        this.text.text = text;
    }
}
