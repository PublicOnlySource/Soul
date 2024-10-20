using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Popup_Inventory : UI_Popup, IPointerClickHandler
{
    [SerializeField]
    private GameObject slotPrefab;
    [SerializeField]
    private Transform rootSlot;
    [SerializeField]
    private UI_InventoryItemDetail detailObj;
    [SerializeField]
    private UI_InventoryClickMenu clickMenuObj;
    [SerializeField]
    private Button hideButton;

    private UI_ItemSlot[] slots;

    private void OnDisable()
    {
        detailObj.Hide();
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveListener<ItemSlotData>(Enums.EventType.UI_ItemSlotData_updatSlot, UpdateSlot);
        EventManager.Instance.RemoveListener<int>(Enums.EventType.UI_int_clearSlot, ClearSlot);
    }

    public override void Init()
    {
        CreateSlot();
        AddEvent();

        detailObj.Hide();
        clickMenuObj.AddRightButtonEvent();
        clickMenuObj.AddLeftButtonEvent((slotIndex) =>
        {
            ItemSlotData data = slots[slotIndex].GetItemData;

            if (data.CheckItemDataType<ConsumableItemData>())
            {
                EventManager.Instance.TriggerEvent<ItemSlotData>(Enums.EventType.UI_ItemSlotData_quickSlot_Register, data);
            }
        });

        hideButton.onClick.AddListener(() =>
        {
            PopupManager.Instance.Hide<UI_Popup_Inventory>();
        });

        UtilManager.Instance.AddButtonEnterSound(hideButton.gameObject);
    }

    private void AddEvent()
    {
        EventManager.Instance.AddListener<ItemSlotData>(Enums.EventType.UI_ItemSlotData_updatSlot, UpdateSlot);
        EventManager.Instance.AddListener<int>(Enums.EventType.UI_int_clearSlot, ClearSlot);
    }

    private void CreateSlot()
    {
        int totalSlot = Constants.Game.INVENTORY_SLOT_ROW * Constants.Game.INVENTORY_SLOT_COLUMN;
        slots = new UI_ItemSlot[totalSlot];

        for (int i = 0; i < totalSlot; i++)
        {
            slots[i] = Instantiate(slotPrefab, rootSlot).GetComponent<UI_ItemSlot>();
            slots[i].Clear();

            slots[i].OnUpdateDetailUIEvent += UpdateDetailUI;
            slots[i].OnUpdateClickMenuEvent += UpdateClickMenuUI;
        }
    }

    private void UpdateSlot(ItemSlotData slotData)
    {       
        slots[slotData.SlotIndex].UpdateSlot(slotData);

        if (slotData.IsRegisterQuickSlot)
        {
            EventManager.Instance.TriggerEvent(Enums.EventType.UI_void_quickSLotUpdate);
        }
    }

    public void ClearSlot(int index)
    {
        slots[index].Clear();
        detailObj.Hide();
        clickMenuObj.Hide();
    }

    public void UpdateDetailUI(ItemSlotData data)
    {
        detailObj.UpdateDetail(data);
        detailObj.Show();
    }

    public void UpdateClickMenuUI(int index, PointerEventData.InputButton clickType)
    {
        if (!slots[index].IsEnable || !slots[index].HasItem)
            return;

        if (clickMenuObj.IsShow())
        {
            clickMenuObj.Hide();
            return;
        }

        RectTransform rt = slots[index].GetRectTransform();
        clickMenuObj.ShowMenu(clickType == PointerEventData.InputButton.Left, index, rt);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clickMenuObj.Hide();
    }
}
