using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeathAction : StateAction
{
    private MonsterAI monsterAI;
    private Damageable damageable;
    private BaseController controller;
    private Attacker attacker;
    private GameObject obj;
    private Coroutine coroutine;

    private readonly float HIDE_TIME = 2f;

    private MonsterDeathActionSO originSO => (MonsterDeathActionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        obj = stateMachine.gameObject;
        monsterAI = stateMachine.GetComponent<MonsterAI>();
        damageable = stateMachine.GetComponent<Damageable>();
        controller = stateMachine.GetComponent<BaseController>();
        attacker = stateMachine.GetComponent<Attacker>();
    }

    public override void OnStateEnter()
    {
        if (controller is MonsterController mc)
        {
            mc.DisableBodyCollider();
        }
        else if (controller is BossController bc)
        {
            bc.DisableCollider();
            CoroutineUtil.Instance.StartCoroutine(ShowDestroyed());
        }

        attacker.DisableCollider();

        CoroutineUtil.Instance.StartCoroutine(HideHealthBar());
        monsterAI.ResetAICoroutine();

        EventManager.Instance.TriggerEvent(Enums.EventType.Player_void_disableLockon);
        EventManager.Instance.TriggerEvent<int>(Enums.EventType.Player_int_add_soul, originSO.GiveSoulValue);
    }

    public override void OnStateExit()
    {
    }

    public override void OnUpdate()
    {
    }

    private IEnumerator HideHealthBar()
    {
        yield return CoroutineUtil.Instance.WaitForSeconds(HIDE_TIME);

        if (!damageable.IsDead)
            yield break;

        damageable.SetActiveHealthBar(false);
    }

    private IEnumerator ShowDestroyed()
    {
        yield return CoroutineUtil.Instance.WaitForSeconds(HIDE_TIME);
        PopupManager.Instance.Show<UI_Popup_Destroyed>();

        BossController bossController = controller as BossController;
        bossController.EnableBonfire();
        bossController.WriteClear();

        UI_Popup_DropItemInfo itemInfo = PopupManager.Instance.Get<UI_Popup_DropItemInfo>();
        itemInfo.SetInfo(Constants.Item.EQUIPMENT_BOSS_SWORD, 1);
        PopupManager.Instance.Show<UI_Popup_DropItemInfo>();

        InGameManager.Instance.PlayerController.PlayerInventory.Add(Constants.Item.EQUIPMENT_BOSS_SWORD, 1);
        DataManager.Instance.SaveGame();
    }
}
