using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = ccm.Debug;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private LayerMask targetLayer;

    [Header("Effect setting(option)")]
    [SerializeField]
    private ParticleSystem effectObj;

    private Collider collider;
    private int addDamage;
    private float damage;

    private void OnEnable()
    {
        if (collider == null)
            collider = GetComponent<Collider>();

        collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((targetLayer & (1 << other.gameObject.layer)) == 0)
            return;

        Vector3 effectPos = other.ClosestPoint(transform.position);
        PerformAttack(other.transform, effectPos);
    }

    private void PerformAttack(Transform target, Vector3 effectPos)
    {
        Damageable damageable;
        float damageResult = damage + addDamage;

        if (!target.TryGetComponent(out damageable))
        {
            target.parent.TryGetComponent(out damageable);
        }

        if (damageable == null)
        {
            Debug.Log("Damageable 컴포넌트 없음");
            return;
        }

        if (damageable.IsDead)
            return;

        if (IsPlayer(target.gameObject))
        {
            if (!ProcessPlayerState(target.gameObject, effectPos, ref damageResult))
                return;
        }

        EffectManager.Instance.ShowEffect(Constants.Prefab.NAME_EFFECT_HIT, effectPos);
        SoundManager.Instance.PlaySFX(Enums.SFX.Hit, Constants.CommonSoundVolume.HALF);
        damageable.TakeDamage(damageResult);
    }

    private bool ProcessPlayerState(GameObject player, Vector3 effectPos, ref float damage)
    {
        if (CheckParry(player))
        {
            Vector3 direction = (effectPos - player.transform.position);
            direction.y = 0;
            
            EffectManager.Instance.ShowEffect(Constants.Prefab.NAME_EFFECT_PARRY, effectPos, Quaternion.LookRotation(direction.normalized));
            SoundManager.Instance.PlaySFX(Enums.SFX.Parry);
            EventManager.Instance.TriggerEvent(Enums.EventType.Monster_void_Parry);
            return false;
        }

        if (CheckBlock(player))
        {
            if (player.TryGetComponent(out PlayerController controller))
            {
                float halfDamage = damage * 0.5f;
                float remainDamage = controller.PlayerStamina.GetUsedRemainValue(halfDamage);
                damage = halfDamage + remainDamage;
            }

            EffectManager.Instance.ShowEffect(Constants.Prefab.NAME_EFFECT_BLOCK, effectPos);
            SoundManager.Instance.PlaySFX(Enums.SFX.Block);
        }

        return true;
    }

    private bool CheckParry(GameObject target)
    {
        if (target.TryGetComponent(out Parry parry))
        {
            if (parry.IsSuccess())
                return true;
        }

        return false;
    }

    private bool CheckBlock(GameObject target)
    {
        if (!InputManager.Instance.IsBlock)
            return false;

        return true;
    }

    private bool IsPlayer(GameObject target)
    {
        return target.layer == LayerMask.NameToLayer(Constants.Game.STRING_PLAYER);
    }

    public void EnableCollider()
    {
        collider.enabled = true;
    }

    public void DisableCollider()
    {
        collider.enabled = false;
    }

    public void AddDamage(int value)
    {
        addDamage = value;
    }

    public void SetDamage(float value)
    {
        damage = value;
    }

    public void ShowEffect()
    {
        if (effectObj == null)
            return;

        effectObj.Play();
    }

    public void HideEffect()
    {
        if (effectObj == null)
            return;

        effectObj.Stop();
    }
}
