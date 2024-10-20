using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotionItem", menuName = "Soul/Items/HealthPotion")]
public class HealthPotionItemSO : ConsumableItemSO
{
    [SerializeField]
    private float recoveryPercentValue;

    public override BaseItem CreateItem()
    {
        return new HealthPotionItem(this);
    }
}
