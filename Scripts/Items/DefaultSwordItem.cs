using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSwordItem : EquipmentItem
{
    private DefaultSwordItemSO defaultSwordItemSO;

    public DefaultSwordItem(DefaultSwordItemSO item) : base(item)
    {
        defaultSwordItemSO = item;
    }
}
