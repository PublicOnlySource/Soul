using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaminaConfig", menuName = "Soul/Config/Stamina Config")]

public class StaminaConfigSO : ScriptableObject
{
    [SerializeField]
    private float initialStamina;
    [SerializeField]
    private float recoveryStaminaDelay;
    [SerializeField]
    private float recoveryStaminaTick;
    [SerializeField]
    private float recoveryStaminaAmount;

    public float InitialStamina { get => initialStamina; }
    public float RecoveryStaminaDelay { get => recoveryStaminaDelay; }
    public float RecoveryStaminaTick { get => recoveryStaminaTick; }
    public float RecoveryStaminaAmount { get => recoveryStaminaAmount; }
}
