using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = ccm.Debug;

public class HealthPotionItem : ConsumableItem
{
    public HealthPotionItem(HealthPotionItemSO item, int amount = 1) : base(item, amount) { }

    public override Enums.ConsumableItemMotionType GetMotionType() => Enums.ConsumableItemMotionType.Drink;
    
    public override void Use()
    {
        Amount--;
        Debug.Log("player revovery health point");
    }
}
