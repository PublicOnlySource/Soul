using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItemData : BaseItemData
{
    public Enums.ItemRank Rank;
    public float Damage;
    public Enums.DamageType DamageType;
    public float Stamina;

    public override void Use()
    {
        EventManager.Instance.TriggerEvent<EquipmentItemData>(Enums.EventType.Player_EquipmentItemData_loadEquipment, this);
    }
}
