using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsDeadCondition : Condition
{
    private BaseController controller;

    public override void Init(StateMachine stateMachine, StateConditionSO originSO)
    {
        base.Init(stateMachine, originSO);
        controller = stateMachine.GetComponent<BaseController>();
    }

    protected override bool Statement()
    {
        return controller.IsDead;
    }
}
