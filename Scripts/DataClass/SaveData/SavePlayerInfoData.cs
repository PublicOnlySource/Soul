using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavePlayerInfoData
{
    public float additionalMaxHealth;
    public float additionalMaxStamina;
    public int additionalDamage;
    public int soul;
    public Vector3 soulDropPosition;
    public int soulDropAmount;
    public Vector3 position;
    public int healthUpgradeLevel;
    public int staminaUpgradeLevel;
    public int damageUpgradeLevel;
    public int potionUpgradeLevel;
    public int potionCountUpgradeLevel;
    public float playTime;
}
