using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventoryItemDetail : MonoBehaviour
{
    [SerializeField]
    private GameObject root;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text typeText;
    [SerializeField]
    private TMP_Text infoTitleText;
    [SerializeField]
    private TMP_Text infoText;
    [SerializeField]
    private TMP_Text descriptionText;
    [SerializeField]
    private Image icon;

    public void UpdateDetail(ItemSlotData data)
    {
        bool isConsumable = data.CheckItemDataType<ConsumableItemData>();

        nameText.text = DataManager.Instance.GetString(data.ItemIndex);
        typeText.text =  DataManager.Instance.GetString(isConsumable ? Constants.StringIndex.ITEM_TYPE_CONSUMABLE : Constants.StringIndex.ITEM_TYPE_EQUIPMENT);
        infoTitleText.text = DataManager.Instance.GetString(isConsumable ? Constants.StringIndex.INFO_TITLE_CONSUMABLE : Constants.StringIndex.INFO_TITLE_EQUIPMENT);
        infoText.text = isConsumable ? data.AmountString : (data.ItemData as EquipmentItemData).Damage.ToString();
        descriptionText.text = DataManager.Instance.GetString(data.ItemData.DescriptionIndex).Replace("\\n", "\n");
        icon.sprite = AddressableManager.Instance.GetSprite(isConsumable ? Constants.Addressable.ATLAS_CONSUMABLE : Constants.Addressable.ATLAS_EQUIPMENT, data.ItemIndexString);
        icon.SetNativeSize();
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
