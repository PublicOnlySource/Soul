using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class BossController : BaseController, IUseBlockByParry
{
    [SerializeField]
    private Enums.MonsterType monsterType;
    [SerializeField]
    private Collider bossCollider;
    [SerializeField]
    private GameObject bonfireObj;

    private Damageable damageable;
    private Detector detector;
    private MonsterAI monsterAI;
    private Attacker attacker;
    private Vector3 originPos;
    private bool isBlockByParry = false;

    public override bool IsDead { get => damageable.IsDead; }
    public bool IsBlockByParry { get => isBlockByParry; set => isBlockByParry = value; }

    public override void Init()
    {
        damageable = GetComponent<Damageable>();
        attacker = GetComponent<Attacker>();
        detector = GetComponent<Detector>();
        monsterAI = GetComponent<MonsterAI>();
        originPos = transform.position;

        MonsterData data = DataManager.Instance.GetMonsterData((int)monsterType);
        if (data != null)
        {
            attacker.SetDamage(data.Damage);
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.AddListener(Enums.EventType.Boss_void_Enter, Enter);
        EventManager.Instance.AddListener(Enums.EventType.Monster_void_spawn, ResetBoss);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener(Enums.EventType.Boss_void_Enter, Enter);
        EventManager.Instance.RemoveListener(Enums.EventType.Monster_void_spawn, ResetBoss);
    }

    private void Enter()
    {
        damageable.SetActiveHealthBar(true);
    }

    private void ResetBoss()
    {
        if (IsDead)
        {
            gameObject.SetActive(false);
            return;
        }

        if (DataManager.Instance.CurrentSaveData.IsClearBoss((int)monsterType))
        {
            gameObject.SetActive(false);
            bonfireObj.SetActive(true);
            return;
        }

        monsterAI.UpdateState(Enums.MonsterAIState.Idle);
        detector.Clear();
        damageable.ResetHealth();
        damageable.SetActiveHealthBar(false);
        detector.EnableDetect();
        transform.localPosition = originPos;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void DisableCollider()
    {
        bossCollider.enabled = false;
    }

    public void EnableCollider()
    {
        bossCollider.enabled = true;
    }

    public void PlayFootSound()
    {
        if (monsterAI.State != Enums.MonsterAIState.Chase)
            return;

        SoundManager.Instance.PlaySFX3D(Enums.SFX.FootStep, Constants.CommonSoundVolume.FOOT_STEP, transform.position);
    }

    public void PlaySwingSound()
    {
        SoundManager.Instance.PlaySFX3D(Enums.SFX.Swing, Constants.CommonSoundVolume.HALF, transform.position);
    }

    public void EnableBonfire()
    {
        bonfireObj.SetActive(true);
    }
        

    public void WriteClear()
    {
        SaveData data = DataManager.Instance.CurrentSaveData;

        data.WriteBossClear((int)monsterType);       
    }
}
