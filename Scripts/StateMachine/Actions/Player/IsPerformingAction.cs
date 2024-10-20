using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPerformingAction : StateAction
{
    private BaseController controller;
    private IsPerformingActionSO originSO => (IsPerformingActionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        controller = stateMachine.GetComponent<BaseController>();
    }

    public override void OnStateEnter()
    {
        if (originSO.WhenToRun == Enums.SpecificMoment.OnStateEnter)
        {
            controller.IsPerformingAction = originSO.BoolValue;
        }
    }

    public override void OnStateExit()
    {
        if (originSO.WhenToRun == Enums.SpecificMoment.OnStateExit)
        {
            controller.IsPerformingAction = originSO.BoolValue;
        }
    }

    public override void OnUpdate()
    {

    }
}
