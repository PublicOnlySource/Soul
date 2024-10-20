using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsItemUseMotionConditon : Condition
{
    private PlayerController controller;
    private IsItemUseMotionConditionSO originSO => (IsItemUseMotionConditionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateConditionSO originSO)
    {
        base.Init(stateMachine, originSO);
        controller = stateMachine.GetComponent<PlayerController>();
    }

    protected override bool Statement()
    {
        return originSO.MotionType == controller.ConsumableItemMotion;
    }
}
