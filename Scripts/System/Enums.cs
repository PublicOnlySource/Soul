using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{
    #region GAME

    public enum Scene
    {
        Initialization,
        Title,
        Loading,
        Main
    }

    public enum SoundType
    {
        Master,
        Bgm,
        Sfx
    }

    public enum BGM
    { 
        Title,
        Main,
        Boss
    }

    public enum SFX
    {
        FootStep,
        Hit,
        Block,
        Parry,
        DrinkPotion,
        Upgrade,
        Swing,
        Hover,
        Death,
        Bonfire,
        BonfireEnable,
    }
    #endregion

    #region ITEM

    public enum DamageType
    {
        Physical,
    }

    public enum ConsumableItemMotionType
    {
        None,
        Use,
        Drink
    }

    public enum ItemFunc
    {
        RecoveryHealth,
        AddSoul
    }

    public enum ItemValueType
    {
        Fixed,
        Percentage,
    }

    public enum ItemRank
    { 
        Rare,
        Epic,
        Legendary
    }

    #endregion

    #region UPGRADE

    public enum UpgradeType
    {
        Health,
        Stamina,
        Damage,
        Potion,
        PotionCount
    }

    #endregion

    #region STATE MACHINE

    public enum ParameterType
    {
        Bool,
        Int,
        Float,
        Trigger
    }

    public enum SpecificMoment
    {
        OnStateEnter,
        OnStateExit,
        OnUpdate,
    }

    public enum InequalitySign
    {
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        Equal
    }

    #endregion

    #region MONSTER

    public enum MonsterAIState
    {
        Idle,
        Patrol,
        Chase,
        Sprint,
        BackJump,
        AttackType1,
        AttackType2,
        AttackType3,
        ComboAttackType1,
        ComboAttackType2, 
        ComboAttackType3,
    }

    public enum MonsterType
    {
        HumanMonster = 100,
        FinalBoss = 101
    }

    #endregion

    #region EVENT TYPE

    // 타겟_제너릭타입_이름
    public enum EventType
    {
        // PLAYER ACTION
        Player_float_recoveryHealth,
        Player_float_recoveryHealthPercentage,
        Player_void_interaction,
        Player_void_restExit,
        Player_void_disableLockon,
        Player_int_add_soul,
        Player_void_death,
        Player_EquipmentItemData_loadEquipment,
        Player_Enums_consumableItemMotionType_SetConsumableItemMotion,
        Player_UpgradePaymentData_upgrade,
        Player_bool_isInSaveArea,

        // INVENTORY
        Inventory_int_use,
        Inventory_int_remove,

        // UI
        UI_ItemSlotData_updatSlot,
        UI_int_clearSlot,
        UI_ItemSlotData_quickSlot_Register,
        UI_void_quickSlotClear,
        UI_void_quickSLotUpdate,
        UI_float_updateStaminaBar,
        UI_float_updateMaxStaminaBar,
        UI_int_updateSoulText,
        UI_bool_setActiveLockOnImage,
        UI_vector3_setPositionLockOnImage,
        UI_void_hideHUD,
        UI_void_showHUD,
        UI_void_completeUpgrade,
        UI_string_updateBossNameText,

        // DATA        
        Data_void_applySaveData,
        Data_void_writeSaveData,

        // GAME
        Monster_void_allDestory,
        Monster_void_spawn,
        Monster_void_Parry,
        Boss_void_Enter,


    }
    #endregion
}
