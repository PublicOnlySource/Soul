using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChangeAIState : StateAction
{
    private MonsterAI monsterAI;

    private MonsterChangeAIStateSO originSO => (MonsterChangeAIStateSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        monsterAI = stateMachine.GetComponent<MonsterAI>();
    }

    public override void OnStateEnter()
    {
        if (originSO.WhenToRun == Enums.SpecificMoment.OnStateEnter)
        {
            monsterAI.UpdateState(originSO.State);
        }
    }

    public override void OnStateExit()
    {
        if (originSO.WhenToRun == Enums.SpecificMoment.OnStateExit)
        {
            monsterAI.UpdateState(originSO.State);
        }
    }

    public override void OnUpdate()
    {
    }
}
