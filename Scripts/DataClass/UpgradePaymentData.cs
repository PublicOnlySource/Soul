using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePaymentData
{
    public int healthUpgradeLevel;
    public int healthUpgradeValue;

    public int staminaUpgradeLevel;
    public int staminaUpgradeValue;

    public int damageUpgradeLevel;
    public int damageUpgradeValue;

    public int potionUpgradeLevel;
    public int potionUpgradeValue;

    public int potionCountUpgradeLevel;
    public int potionCountUpgradeValue;

    public int totalSoul;

    public bool IsZero()
    {
        return totalSoul == 0;
    }
}
