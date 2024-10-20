using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotData
{
    private int slotIndex;
    private int amount;
    private BaseItemData itemData;
    private bool isWorn = false;
    private bool isRegisterQuickSlot = false;

    public BaseItemData ItemData { get => itemData; }
    public int SlotIndex { get => slotIndex; }
    public int ItemIndex { get => itemData.Index; }
    public int Amount {  get => amount; }
    public bool IsWorn { get => isWorn; set => isWorn = value; }
    public bool IsRegisterQuickSlot { get => isRegisterQuickSlot; set => isRegisterQuickSlot = value; }
    public string ItemIndexString { get => itemData.Index.ToString(); }
    public string AmountString { get => amount.ToString(); }

    public ItemSlotData(int slotIndex, int amount, BaseItemData itemData)
    {
        this.slotIndex = slotIndex;
        this.amount = amount;
        this.itemData = itemData;
    }

    public ItemSlotData(SaveInventoryData saveData, BaseItemData itemData)
    {
        this.slotIndex=saveData.slotIndex;
        this.amount=saveData.amount;
        this.itemData = itemData;
        this.isWorn = saveData.isWorn;
        this.isRegisterQuickSlot = saveData.isRegisterQuickSlot;
    }

    public T GetItemData<T>() where T : BaseItemData
    {
        if (itemData is T)
            return itemData as T;

        return null;
    }

    public bool CheckItemDataType<T>() where T : BaseItemData
    {
        return itemData is T;
    }

    public void AddAmount(int amount)
    {
       this.amount += amount;
    }

    public bool UseAmount(int amount)
    {
        if (this.amount - amount < 0)
            return false;

        this.amount -= amount;
        return true;
    }

    public bool IsZeroAmount()
    {
        return amount == 0;
    }

}
