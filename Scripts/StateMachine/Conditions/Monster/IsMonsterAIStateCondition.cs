using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsMonsterAIStateCondition : Condition
{
    private MonsterAI monsterAi;
    private IsMonsterAIStateConditionSO originSO => (IsMonsterAIStateConditionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateConditionSO originSO)
    {
        base.Init(stateMachine, originSO);
        monsterAi = stateMachine.GetComponent<MonsterAI>();
    }

    protected override bool Statement()
    {
        return monsterAi.State == originSO.TargetState;
    }
}
