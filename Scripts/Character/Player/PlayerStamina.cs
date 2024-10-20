using System.Collections;
using UnityEngine;

public class PlayerStamina
{
    private StaminaConfigSO staminaConfigSO;
    private Coroutine recoveryCoroutine;
    private float stamina;
    private float additionalMaxStamina;

    public float Stamina {  get => stamina; }
    public float MaxStamina { get => staminaConfigSO.InitialStamina + additionalMaxStamina; }

    public PlayerStamina(StaminaConfigSO staminaConfigSO)
    {
        this.staminaConfigSO = staminaConfigSO;
        stamina = staminaConfigSO.InitialStamina;
    }
    
    private void StartRecoveryProcess()
    {
       ClearCoroutine();

        recoveryCoroutine = CoroutineUtil.Instance.StartCoroutine(RecoveryStamina());
    }

    private void ClearCoroutine()
    {
        if (recoveryCoroutine != null)
        {
            CoroutineUtil.Instance.StopCoroutine(recoveryCoroutine);
            recoveryCoroutine = null;
        }
    }

    private IEnumerator RecoveryStamina()
    {
        yield return CoroutineUtil.Instance.WaitForSeconds(staminaConfigSO.RecoveryStaminaDelay);

        while (true)
        {
            if (stamina >= MaxStamina)
                break;

            yield return CoroutineUtil.Instance.WaitForSeconds(staminaConfigSO.RecoveryStaminaTick);

            stamina += staminaConfigSO.RecoveryStaminaAmount;
            if (stamina > MaxStamina)
                stamina = MaxStamina;

            EventManager.Instance.TriggerEvent<float>(Enums.EventType.UI_float_updateStaminaBar, stamina);
        }
    }

    public bool Use(float value)
    {
        if (stamina - value < 0)
            return false;

        stamina -= value;
        EventManager.Instance.TriggerEvent<float>(Enums.EventType.UI_float_updateStaminaBar, stamina);

        StartRecoveryProcess();
       
        return true;
    }

    public float GetUsedRemainValue(float value)
    {
        float remain = stamina - value;

        if (remain >= 0)
        {
            Use(value);
            return 0;
        }

        Use(value + remain);

        return Mathf.Abs(remain);
    }

    public void SetMaxStamina(float value)
    {
        additionalMaxStamina = value;
        Init();

    }

    public void Reset()
    {
        ClearCoroutine();
        stamina = MaxStamina;
        EventManager.Instance.TriggerEvent<float>(Enums.EventType.UI_float_updateStaminaBar, stamina);
    }

    public void Init()
    {
        EventManager.Instance.TriggerEvent<float>(Enums.EventType.UI_float_updateMaxStaminaBar, staminaConfigSO.InitialStamina + additionalMaxStamina);
        EventManager.Instance.TriggerEvent<float>(Enums.EventType.UI_float_updateStaminaBar, stamina);
    }
}
