using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    [Header("Setting")]
    [SerializeField]
    private StaminaConfigSO staminaConfigSO;
    [SerializeField]
    private Transform handRoot;

    private Enums.ConsumableItemMotionType consumableItemMotion;
    private Damageable damageable;
    private Attacker attacker;
    private PlayerSoul playerSoul;
    private PlayerStamina playerStamina;
    private PlayerInventory playerInventory;
    private PlayerEquitment playerEquitment;
    private int additionalPotionCount;
    private int additionalPotionRecoveryValue;

    public Enums.ConsumableItemMotionType ConsumableItemMotion { get => consumableItemMotion; }
    public PlayerSoul PlayerSoul { get => playerSoul; }
    public PlayerStamina PlayerStamina { get => playerStamina; }
    public PlayerInventory PlayerInventory { get => playerInventory; }
    public PlayerEquitment PlayerEquitment { get => playerEquitment; }
    public Damageable Damageable { get => damageable; }
    public override bool IsDead { get => damageable.IsDead; }
    public int AdditionalPotionCount { get => additionalPotionCount; }
    public Transform HandRoot { get => handRoot; }

    [HideInInspector]
    public bool IsInSaveArea = false;
    [HideInInspector]
    public bool IsRest = false;

    private void Start()
    {
        playerStamina.Init();
    }

    private void OnEnable()
    {
        damageable = GetComponent<Damageable>();
        attacker = GetComponent<Attacker>();
        CreateRequireClass();

        playerInventory.AddListeners();
        playerSoul.AddListeners();
        playerEquitment.AddListeners();
        EventManager.Instance.AddListener(Enums.EventType.Player_void_restExit, EvnetExitRest);
        EventManager.Instance.AddListener(Enums.EventType.Data_void_applySaveData, ApplyData);
        EventManager.Instance.AddListener<float>(Enums.EventType.Player_float_recoveryHealth, RecoveryHealth);
        EventManager.Instance.AddListener<float>(Enums.EventType.Player_float_recoveryHealthPercentage, RecoveryHealthPercentage);
        EventManager.Instance.AddListener<Enums.ConsumableItemMotionType>(Enums.EventType.Player_Enums_consumableItemMotionType_SetConsumableItemMotion, SetConsumableItemMotion);
        EventManager.Instance.AddListener<UpgradePaymentData>(Enums.EventType.Player_UpgradePaymentData_upgrade, Upgrade);
    }

    private void OnDisable()
    {
        playerInventory.RemoveListeners();
        playerSoul.RemoveListeners();
        playerEquitment.RemoveListeners();
        EventManager.Instance.RemoveListener(Enums.EventType.Player_void_restExit, EvnetExitRest);
        EventManager.Instance.RemoveListener(Enums.EventType.Data_void_applySaveData, ApplyData);
        EventManager.Instance.RemoveListener<float>(Enums.EventType.Player_float_recoveryHealth, RecoveryHealth);
        EventManager.Instance.RemoveListener<float>(Enums.EventType.Player_float_recoveryHealthPercentage, RecoveryHealthPercentage);
        EventManager.Instance.RemoveListener<Enums.ConsumableItemMotionType>(Enums.EventType.Player_Enums_consumableItemMotionType_SetConsumableItemMotion, SetConsumableItemMotion);
        EventManager.Instance.RemoveListener<UpgradePaymentData>(Enums.EventType.Player_UpgradePaymentData_upgrade, Upgrade);
    }

    private void CreateRequireClass()
    {
        if (playerEquitment == null)
        {
            playerEquitment = new PlayerEquitment(handRoot, attacker);
        }

        if (playerStamina == null)
        {
            playerStamina = new PlayerStamina(staminaConfigSO);
        }

        if (playerSoul == null)
        {
            playerSoul = new PlayerSoul();
        }

        if (playerInventory == null)
        {
            playerInventory = new PlayerInventory(this, handRoot);
        }
    }

    private void EvnetExitRest()
    {
        IsRest = false;
    }

    private void SetConsumableItemMotion(Enums.ConsumableItemMotionType type)
    {
        consumableItemMotion = type;
    }

    private void RecoveryHealth(float value)
    {
        damageable.RecoveryHealth(value);
    }

    private void RecoveryHealthPercentage(float value)
    {
        float max = damageable.HealthData.MaxHealth;
        float percentage = value + additionalPotionRecoveryValue;
        RecoveryHealth(max * 0.01f * percentage);
    }

    private void Upgrade(UpgradePaymentData data)
    {
        if (data.IsZero())
            return;

        if (!playerSoul.UseSoul(data.totalSoul))
            return;

        damageable.HealthData.SetMaxHealth(data.healthUpgradeValue);
        playerStamina.SetMaxStamina(data.staminaUpgradeValue);
        attacker.AddDamage(data.damageUpgradeValue);
        additionalPotionCount = data.potionCountUpgradeValue;
        additionalPotionRecoveryValue = data.potionUpgradeValue;

        EffectManager.Instance.ShowEffect(Constants.Prefab.NAME_EFFECT_UPGRADE, transform.position);
        SoundManager.Instance.PlaySFX(Enums.SFX.Upgrade);
        DataManager.Instance.CurrentSaveData.WriteUpgradeLevel(data);

        EventManager.Instance.TriggerEvent(Enums.EventType.UI_void_completeUpgrade);
        
        EventManager.Instance.TriggerEvent(Enums.EventType.Data_void_writeSaveData);
        DataManager.Instance.SaveGame();
    }

    private void ApplyData()
    {
        SaveData data = DataManager.Instance.CurrentSaveData;
        int additionalMaxHealthValue = DataManager.Instance.GetUpgradeValueByLevel(Enums.UpgradeType.Health, data.HealthUpgradeLevel);
        int additionalMaxStaminaValue = DataManager.Instance.GetUpgradeValueByLevel(Enums.UpgradeType.Stamina, data.StaminaUpgradeLevel);
        int additionalDamageValue = DataManager.Instance.GetUpgradeValueByLevel(Enums.UpgradeType.Damage, data.DamageUpgradeLevel);

        SetPosition(data.PlayerPosition);
        damageable.HealthData.SetMaxHealth(additionalMaxHealthValue);
        playerStamina.SetMaxStamina(additionalMaxStaminaValue);
        attacker.AddDamage(additionalDamageValue);
        additionalPotionCount = DataManager.Instance.GetUpgradeValueByLevel(Enums.UpgradeType.PotionCount, data.PotionCountUpgradeLevel);
        additionalPotionRecoveryValue = DataManager.Instance.GetUpgradeValueByLevel(Enums.UpgradeType.Potion, data.PotionUpgradeLevel);

        ResetState();
    }

    public void SetPosition(Vector3 pos)
    {
        CharacterController cc = GetComponent<CharacterController>();
        cc.enabled = false;
        transform.position = pos;
        cc.enabled = true;
    }

    public void Reborn()
    {
        ResetState();
        playerSoul.SetSoul(0);
    }

    public void ResetState()
    {
        damageable.ResetHealth();
        playerStamina.Reset();
    }

    public void PlayFootSound()
    {
        if (IsPerformingAction)
            return;

        if (InputManager.Instance.MoveAmount > 0)
        {
            SoundManager.Instance.PlaySFX(Enums.SFX.FootStep, Constants.CommonSoundVolume.FOOT_STEP);
        }
    }

    public void PlaySwingSound()
    {
        SoundManager.Instance.PlaySFX(Enums.SFX.Swing);
    }
}