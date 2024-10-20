using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup_Update : UI_Popup
{
    [SerializeField]
    private Button okButton;
    [SerializeField]
    private TMP_Text infoText;

    public override void Init()
    {
        base.Init();
        okButton.onClick.AddListener(() => Patch());
    }

    public void UpdatePercentage(float value)
    {
        infoText.text = $"{value} %";
    }    

    public void ShowDownloadSize(float size)
    {
        infoText.text = $"{size:F2} MB";
    }

    public void Patch()
    {
        AddressableManager.Instance.ProcessPatch();
    }

}
