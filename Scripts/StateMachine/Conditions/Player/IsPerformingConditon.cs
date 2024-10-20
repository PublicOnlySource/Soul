using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPerformingConditon : Condition
{
    private BaseController controller;

    private IsPerformingConditionSO originSO => (IsPerformingConditionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateConditionSO originSO)
    {
        base.Init(stateMachine, originSO);
        controller = stateMachine.GetComponent<BaseController>();
    }

    protected override bool Statement()
    {
        return controller.IsPerformingAction;
    }
}
