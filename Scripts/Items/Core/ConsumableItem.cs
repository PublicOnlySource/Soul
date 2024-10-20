using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumableItem : BaseItem
{
    private int amount;
    private ConsumableItemSO consumableItemSO;

    public int Amount { 
        get => amount; 
        set
        {
            if (value > amount) amount = value;
            else if (value < 0) amount = 0;

            amount = value;
        }
    }

    public ConsumableItemSO ConsumableItemSO { get => consumableItemSO; }

    public ConsumableItem(ConsumableItemSO item, int amount = 1) : base(item)
    {
        this.consumableItemSO = item;
        this.amount = amount;   
    }

    public bool IsEmpty()
    {
        return amount == 0;
    }

    public abstract void Use();
    public abstract Enums.ConsumableItemMotionType GetMotionType();
}
