using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultSword", menuName = "Soul/Items/Weapons/DefaultSword")]
public class DefaultSwordItemSO : EquipmentItemSO
{
    public override BaseItem CreateItem()
    {
        return new DefaultSwordItem(this);
    }
}
