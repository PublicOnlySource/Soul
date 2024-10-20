using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentItemSO : BaseItemSO
{
    [SerializeField]
    private Enums.DamageType damageType;
    [SerializeField]
    private float defaultDamage;
    [SerializeField]
    private float staminaCost;

    public float StaminaCost { get => staminaCost; }
    public float DefaultDamage { get => defaultDamage; }
    public Enums.DamageType DamageType { get => damageType; }
}
