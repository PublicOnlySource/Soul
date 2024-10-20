using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BaseController, IUseBlockByParry
{
    [SerializeField]
    private Enums.MonsterType monsterType;
    [SerializeField]
    private Collider monsterCollider;

    [Header("Settings")]
    [SerializeField]
    private float returnDistance = 10f;

    private Damageable damageable;
    private Attacker attacker;
    private Vector3 createdPos;
    private bool isBlockByParry = false;

    public override bool IsDead { get => damageable.IsDead; }
    public float ReturnDistance { get => returnDistance; }
    public Vector3 CreatedPos { get =>  createdPos; }
    public bool IsBlockByParry { get => isBlockByParry; set => isBlockByParry = value; }

    public override void Init()
    {
        attacker = GetComponent<Attacker>();

        MonsterData data = DataManager.Instance.GetMonsterData((int)monsterType);  
        if (data != null)
        {
            attacker.SetDamage(data.Damage);
        }
    }

    private void OnEnable()
    {
        damageable = GetComponent<Damageable>();
        createdPos = transform.position;
        EnableBodyCollider();
    }

    public void PlayFootSound()
    {
        SoundManager.Instance.PlaySFX3D(Enums.SFX.FootStep, Constants.CommonSoundVolume.FOOT_STEP, transform.position);
    }

    public void PlaySwingSound()
    {
        SoundManager.Instance.PlaySFX3D(Enums.SFX.Swing, Constants.CommonSoundVolume.HALF, transform.position);
    }

    public void DisableBodyCollider()
    {
        monsterCollider.enabled = false;
    }

    public void EnableBodyCollider()
    {
        monsterCollider.enabled = true;
    }
}
