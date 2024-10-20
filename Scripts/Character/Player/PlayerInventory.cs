using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = ccm.Debug;

public class PlayerInventory : IUseEventNonMono
{
    private ItemSlotData[] items;
    private PlayerController playerController;
    private Transform handRoot;
    private int currentWornSlotIndex = -1;

    public PlayerInventory(PlayerController playerController, Transform handRoot)
    {
        this.playerController = playerController;
        items = new ItemSlotData[Constants.Game.INVENTORY_SLOT_ROW * Constants.Game.INVENTORY_SLOT_COLUMN];
        this.handRoot = handRoot;
    }

    private void UpdateSlot(int index)
    {
        EventManager.Instance.TriggerEvent<ItemSlotData>(Enums.EventType.UI_ItemSlotData_updatSlot, items[index]);

        if (!items[index].CheckItemDataType<ConsumableItemData>())
            return;

        if (!items[index].IsZeroAmount())
            return;

        RemoveItem(index);
    }

    private void RemoveItem(int index)
    {
        if (items[index].IsWorn)
            return;

        if (items[index].IsRegisterQuickSlot)
        {
            EventManager.Instance.TriggerEvent(Enums.EventType.UI_void_quickSlotClear);
        }

        EventManager.Instance.TriggerEvent<int>(Enums.EventType.UI_int_clearSlot, index);
        items[index] = null;
    }

    private int FindConsumableItemSlotByItemIndex(int itemIndex)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].ItemIndex == itemIndex)
            {
                if (items[i].CheckItemDataType<ConsumableItemData>())
                    return i;

                break;
            }
        }

        return -1;
    }

    private int FindEmptySlot()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
                return i;
        }

        return -1;
    }

    public bool Add(int itemIndex, int amount = 1)
    {
        int index = FindConsumableItemSlotByItemIndex(itemIndex);

        if (index == -1)
        {
            index = FindEmptySlot();

            if (index == -1)
            {
                Debug.Log("Full Inventory");
                return false;
            }

            BaseItemData itemData = DataManager.Instance.GetItemData(itemIndex);
            ItemSlotData slotData = new ItemSlotData(index, amount, itemData);
            items[index] = slotData;
        }
        else
        {
            items[index].AddAmount(amount);
        }

        UpdateSlot(index);
        return true;
    }

    public void Use(int slotIndex)
    {
        if (items[slotIndex] == null || playerController.IsPerformingAction)
            return;

        if (items[slotIndex].CheckItemDataType<EquipmentItemData>())
        {
            if (currentWornSlotIndex != -1)
            {
                items[currentWornSlotIndex].IsWorn = false;
                currentWornSlotIndex = slotIndex;
            }

            items[slotIndex].IsWorn = true;
        }
        else if (items[slotIndex].CheckItemDataType<ConsumableItemData>())
        {
            items[slotIndex].UseAmount(1);
        }

        items[slotIndex].ItemData.Use();
        UpdateSlot(slotIndex);
    }

    public int GetAmountByItemIndex(int index)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
                continue;

            if (items[i].ItemIndex == index)
                return items[i].Amount;
        }

        return 0;
    }

    public void WriteData()
    {
        SaveData saveData = DataManager.Instance.CurrentSaveData;
        saveData.WriteInventory(items);
    }

    public void ApplyData()
    {
        SaveData data = DataManager.Instance.CurrentSaveData;
        List<SaveInventoryData> loadDatas = data.Inventory;
        ItemSlotData slotData;
        BaseItemData itemData;

        for (int i = 0; i < loadDatas.Count; i++)
        {
            itemData = DataManager.Instance.GetItemData(loadDatas[i].itemIndex);
            slotData = new ItemSlotData(loadDatas[i], itemData);
            items[loadDatas[i].slotIndex] = slotData;

            if (loadDatas[i].isWorn)
            {
                Use(loadDatas[i].slotIndex);
                currentWornSlotIndex = i;
            }

            if (loadDatas[i].isRegisterQuickSlot)
            {
                EventManager.Instance.TriggerEvent<ItemSlotData>(Enums.EventType.UI_ItemSlotData_quickSlot_Register, items[loadDatas[i].slotIndex]);
            }

            UpdateSlot(i);
        }
    }

    public void AddListeners()
    {
        EventManager.Instance.AddListener<int>(Enums.EventType.Inventory_int_use, Use);
        EventManager.Instance.AddListener<int>(Enums.EventType.Inventory_int_remove, RemoveItem);
        EventManager.Instance.AddListener(Enums.EventType.Data_void_writeSaveData, WriteData);
        EventManager.Instance.AddListener(Enums.EventType.Data_void_applySaveData, ApplyData);
    }

    public void RemoveListeners()
    {
        EventManager.Instance.RemoveListener<int>(Enums.EventType.Inventory_int_use, Use);
        EventManager.Instance.RemoveListener<int>(Enums.EventType.Inventory_int_remove, RemoveItem);
        EventManager.Instance.RemoveListener(Enums.EventType.Data_void_writeSaveData, WriteData);
        EventManager.Instance.RemoveListener(Enums.EventType.Data_void_applySaveData, ApplyData);
    }
}
