using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackType1Action : StateAction
{
    private IUseBlockByParry blockByParry;
    private MonsterAI monsterAI;
    private Coroutine coroutine;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        monsterAI = stateMachine.GetComponent<MonsterAI>();
        blockByParry = stateMachine.GetComponent<IUseBlockByParry>();
    }

    public override void OnStateEnter()
    {
        EventManager.Instance.AddListener(Enums.EventType.Monster_void_Parry, BlockByParry);
        coroutine = CoroutineUtil.Instance.StartCoroutine(Attack());
    }

    public override void OnStateExit()
    {
        EventManager.Instance.RemoveListener(Enums.EventType.Monster_void_Parry, BlockByParry);
        CoroutineUtil.Instance.StopCoroutine(coroutine);
    }

    public override void OnUpdate()
    {
    }

    private IEnumerator Attack()
    {
        monsterAI.DisableAgent();
        yield return null;
    }

    private void BlockByParry()
    {
        blockByParry.IsBlockByParry = true;
    }
}
