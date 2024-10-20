using System.Collections;
using UnityEngine;

public class MonsterIdleAction : StateAction
{
    private MonsterController monsterController;
    private MonsterAI monsterAI;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        monsterAI = stateMachine.GetComponent<MonsterAI>();
        monsterController = stateMachine.GetComponent<MonsterController>();
    }

    public override void OnStateEnter()
    {
        monsterAI.Move(monsterAI.transform.position);
        IsOverDistanceFromOriginPos();
    }

    public override void OnStateExit()
    {
    }

    public override void OnUpdate()
    {
    }

    private void IsOverDistanceFromOriginPos()
    {
        if (Vector3.Distance(monsterController.CreatedPos, monsterController.transform.position) > monsterController.ReturnDistance)
        {
            monsterAI.UpdateState(Enums.MonsterAIState.Patrol);            
        }
    }
}