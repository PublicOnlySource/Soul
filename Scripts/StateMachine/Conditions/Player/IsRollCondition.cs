using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsRollCondition : Condition
{
    private PlayerController playerController;

    private IsRollConditionSO originSO => (IsRollConditionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateConditionSO originSO)
    {
        base.Init(stateMachine, originSO);
        playerController = stateMachine.GetComponent<PlayerController>();
    }

    protected override bool Statement()
    {
        if (playerController.PlayerStamina.Stamina < originSO.PlayerActionConfigSO.RollCost)
            return false;

        return InputManager.Instance.RollInput && !playerController.IsPerformingAction;
    }
}
