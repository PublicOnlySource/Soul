using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField]
    private Image iconImage;
    [SerializeField]
    private TMP_Text amountText;
    [SerializeField]
    private Image disableImage;

    private bool isEnable;
    private ItemSlotData itemSlotData;
    private RectTransform rt;
    private Action<ItemSlotData> updateDetailUIEvent;
    private Action<int, PointerEventData.InputButton> updateClickMenuEvent;

    public Action<ItemSlotData> OnUpdateDetailUIEvent { get => updateDetailUIEvent; set => updateDetailUIEvent = value; }
    public Action<int, PointerEventData.InputButton> OnUpdateClickMenuEvent { get => updateClickMenuEvent; set => updateClickMenuEvent = value; }
    public bool HasItem => itemSlotData != null;
    public bool IsEnable => isEnable;
    public ItemSlotData GetItemData => itemSlotData;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();    
    }

    private void UpdateIcon()
    {
        string atlasName = itemSlotData.CheckItemDataType<ConsumableItemData>() ? Constants.Addressable.ATLAS_CONSUMABLE : Constants.Addressable.ATLAS_EQUIPMENT;
        iconImage.sprite = AddressableManager.Instance.GetSprite(atlasName, itemSlotData.ItemIndex.ToString());
        ShowIcon();
    }

    private void UpdateAmountText()
    {
        if (itemSlotData.CheckItemDataType<EquipmentItemData>())
        {
            HideAmountText();
            return;
        }

        if (itemSlotData.Amount <= 0)
        {
            Clear();
            return;
        }

        amountText.text = itemSlotData.AmountString;
        ShowAmountText();
    }

    public void UpdateSlot(ItemSlotData itemData)
    {
        this.itemSlotData = itemData;
        UpdateIcon();
        UpdateAmountText();
        Enable();
    }

    public void ShowAmountText()
    {
        amountText.gameObject.SetActive(true);
    }

    public void HideAmountText()
    {
        amountText.gameObject.SetActive(false);
    }

    public string GetAmountText()
    {
        return amountText.text;
    }

    public void HideIcon()
    {
        iconImage.gameObject.SetActive(false);
    }

    public void ShowIcon()
    {
        iconImage.gameObject.SetActive(true);
    }

    public void Enable()
    {
        disableImage.gameObject.SetActive(false);
        isEnable = true;
    }

    public void Disable()
    {
        disableImage.gameObject.SetActive(true);
        isEnable = false;
    }

    public void Clear()
    {
        itemSlotData = null;
        iconImage.sprite = null;
        amountText.text = string.Empty;
        HideAmountText();
        HideIcon();
        Disable();
    }

    public RectTransform GetRectTransform()
    {
        return rt;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!HasItem) return;

        OnUpdateDetailUIEvent(itemSlotData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!HasItem) return;

        OnUpdateClickMenuEvent(itemSlotData.SlotIndex, eventData.button);
    }
}
