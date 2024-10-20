using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockByParryAction : StateAction
{
    private IUseBlockByParry blockByParry;
    private MonsterAI MonsterAI;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        blockByParry = stateMachine.GetComponent<IUseBlockByParry>();
        MonsterAI = stateMachine.GetComponent<MonsterAI>();
    }

    public override void OnStateEnter()
    {
        blockByParry.IsBlockByParry = false;
    }

    public override void OnStateExit()
    {
        MonsterAI.UpdateState(Enums.MonsterAIState.Idle);
    }

    public override void OnUpdate()
    {
    }
}
