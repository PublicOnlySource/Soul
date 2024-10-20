using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Health", menuName = "Soul/Character/Health")]
public class HealthSO : ScriptableObject
{
    [SerializeField]
    [ReadOnly] 
    private float maxHealth;

    [SerializeField]
    [ReadOnly] 
    private float currentHealth;

    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;

    public void SetMaxHealth(float newValue)
    {
        maxHealth = newValue;
    }

    public void SetCurrentHealth(float newValue)
    {
        currentHealth = newValue;
    }

    public void TakeDamage(float DamageValue)
    {
        currentHealth -= DamageValue;
    }

    public void RestoreHealth(float HealthValue)
    {
        currentHealth += HealthValue;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
}
