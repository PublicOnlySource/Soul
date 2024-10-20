using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHitCondition : Condition
{
    private Damageable damageable;

    public override void Init(StateMachine stateMachine, StateConditionSO originSO)
    {
        base.Init(stateMachine, originSO);
        damageable = stateMachine.GetComponent<Damageable>();
    }

    protected override bool Statement()
    {
        return damageable.IsHit;
    }
}
