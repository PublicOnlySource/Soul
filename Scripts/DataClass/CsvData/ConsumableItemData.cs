using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConsumableItemData : BaseItemData
{
    public Enums.ItemFunc Func;
    public int Value;
    public Enums.ItemValueType ValueType;
    public Enums.ConsumableItemMotionType AnimationType;

    public override void Use()
    {
        switch (Func)
        {
            case Enums.ItemFunc.RecoveryHealth:
                Enums.EventType type = ValueType == Enums.ItemValueType.Fixed ? Enums.EventType.Player_float_recoveryHealth : Enums.EventType.Player_float_recoveryHealthPercentage;
                EventManager.Instance.TriggerEvent<float>(type, Value);
                                
                EventManager.Instance.TriggerEvent<Enums.ConsumableItemMotionType>(Enums.EventType.Player_Enums_consumableItemMotionType_SetConsumableItemMotion, Enums.ConsumableItemMotionType.Drink);

                Transform player = InGameManager.Instance.PlayerController.transform;
                EffectManager.Instance.ShowEffect(Constants.Prefab.NAME_EFFECT_DRINK_POTION, player.localPosition, player);
                SoundManager.Instance.PlaySFX(Enums.SFX.DrinkPotion);
                break;
            case Enums.ItemFunc.AddSoul:
                EventManager.Instance.TriggerEvent<Enums.ConsumableItemMotionType>(Enums.EventType.Player_Enums_consumableItemMotionType_SetConsumableItemMotion, Enums.ConsumableItemMotionType.Use);
                EventManager.Instance.TriggerEvent(Enums.EventType.Player_int_add_soul, Value);
                break;
        }
    }
}
