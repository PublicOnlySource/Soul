using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup_DropItemInfo : UI_Popup
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text amountText;

    private readonly int AUTO_TIME = 3;

    public override void Show()
    {
        base.Show();
        StartCoroutine(AutoHide());
    }

    public void SetInfo(int itemIndex, int amount)
    {
        BaseItemData itemData = DataManager.Instance.GetItemData(itemIndex);
        string atlasName = itemData is EquipmentItemData ? Constants.Addressable.ATLAS_EQUIPMENT : Constants.Addressable.ATLAS_CONSUMABLE;

        AddressableManager.Instance.GetSprite(atlasName, itemData.Index.ToString());
        nameText.text = DataManager.Instance.GetString(itemData.Index);
        amountText.text = amount.ToString();
    }

    private IEnumerator AutoHide()
    {
        yield return CoroutineUtil.Instance.WaitForSeconds(AUTO_TIME);
        PopupManager.Instance.Hide<UI_Popup_DropItemInfo>();
    }
}
