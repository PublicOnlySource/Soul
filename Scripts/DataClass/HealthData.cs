using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthData
{
    private HealthConfigSO healthConfig;
    private float health;
    private float additionalMaxHealth;

    public float MaxHealth { get => healthConfig.InitialHealth + additionalMaxHealth; }
    public float CurrentHealth { get => health; }
    public bool IsDead { get => health <= 0; }

    public Action<float> updateHealthBarEvent;
    public Action<float> changeHealthBarMaxEvnet;
    public Action<bool> setActiveHealthBarEvent;

    public HealthData(HealthConfigSO healthConfigSO)
    {
        this.healthConfig = healthConfigSO;
        health = healthConfigSO.InitialHealth;
    }

    public void Init()
    {
        changeHealthBarMaxEvnet?.Invoke(MaxHealth);
        updateHealthBarEvent?.Invoke(health);
    }

    public void SetMaxHealth(float value)
    {
        additionalMaxHealth = value;
        changeHealthBarMaxEvnet?.Invoke(MaxHealth);
        updateHealthBarEvent?.Invoke(health);
    }

    public void TakeDamage(float DamageValue)
    {
        if (IsDead)
            return;

        health -= DamageValue;
        updateHealthBarEvent?.Invoke(health);
    }

    public void AddHealth(float value)
    {
        health += value;

        if (health > MaxHealth)
        {
            health = MaxHealth;
        }

        updateHealthBarEvent?.Invoke(health);
    }

    public void Reset()
    {
        health = MaxHealth;
        updateHealthBarEvent?.Invoke(health);
    }

    public void SetActiveHealthBar(bool value)
    {
        setActiveHealthBarEvent?.Invoke(value);
    }
}
