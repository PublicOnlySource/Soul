using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAction : StateAction
{      
    private PlayerController controller;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        controller = stateMachine.GetComponent<PlayerController>();
    }

    public override void OnStateEnter()
    {
        float attackCost = controller.PlayerEquitment.ItemData.Stamina;
        controller.PlayerStamina.Use(attackCost);
    }

    public override void OnStateExit()
    {
      
    }

    public override void OnUpdate()
    {
      
    }
}
