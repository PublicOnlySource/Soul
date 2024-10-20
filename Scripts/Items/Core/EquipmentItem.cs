using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : BaseItem
{
    private EquipmentItemSO equimentItemSO;

    public EquipmentItem(EquipmentItemSO item) : base(item)
    {
        this.equimentItemSO = item;
    }


    public int GetId() => equimentItemSO.Id;
    public GameObject GetPrefab() => equimentItemSO.Prefab;
}
