using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Soul/Config/Damage Config")]
public class DamageConfigSO : ScriptableObject
{
    public float baseDamage;
    public Enums.DamageType damageType;
    public LayerMask damageTargetLayer;
}
