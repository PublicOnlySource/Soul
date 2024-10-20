using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterFollowPlayerAction : StateAction
{
    private MonsterAI monsterAI;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        monsterAI = stateMachine.GetComponent<MonsterAI>();
    }

    public override void OnStateEnter()
    {

    }

    public override void OnStateExit()
    {

    }

    public override void OnUpdate()
    {
        monsterAI.MoveDetectObject();
    }
}
