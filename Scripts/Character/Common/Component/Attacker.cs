using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{    
    [SerializeField]
    private Attack attack;

    private int addDamage;

    public void EnableCollider()
    {
        attack.EnableCollider();
    }

    public void DisableCollider()
    {
        attack.DisableCollider();
    }

    public void SetAttackComponent(Transform weapon, float damage)
    {
        attack = weapon.GetComponent<Attack>();
        attack.SetDamage(damage);
        attack.AddDamage(addDamage);
        DisableCollider();
    }

    public void ResetAttackComponent()
    {
        DisableCollider();
        attack = null;
    }

    public void AddDamage(int value)
    {
        addDamage += value;
    }

    public void SetDamage(float value)
    {
        attack.SetDamage(value);
    }

    public void ShowEffect()
    {
        attack.ShowEffect();
    }

    public void HideEffect()
    {
        attack.HideEffect();
    }
}
