using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementAciton : StateAction
{
    private CharacterController characterController;
    private PlayerController playerController;
    private Vector3 moveDirection;

    private PlayerMovementActionSO originSO => (PlayerMovementActionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        characterController = stateMachine.GetComponent<CharacterController>();
        playerController = stateMachine.GetComponent<PlayerController>();
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }

    public override void OnUpdate()
    {
        if (playerController.IsPerformingAction)
            return;

        moveDirection = FollowCameraComponent.Instance.transform.forward * InputManager.Instance.VerticalInput;
        moveDirection += FollowCameraComponent.Instance.transform.right * InputManager.Instance.HorizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (InputManager.Instance.MoveAmount <= 1)
        {
            characterController.Move(moveDirection * originSO.PlayerActionConfigSO.WalkSpeed * Time.deltaTime);
        }
        else if (playerController.PlayerStamina.Stamina >= originSO.PlayerActionConfigSO.SprintCost && InputManager.Instance.MoveAmount > 1)
        {
            characterController.Move(moveDirection * originSO.PlayerActionConfigSO.SprintSpeed * Time.deltaTime);
            playerController.PlayerStamina.Use(originSO.PlayerActionConfigSO.SprintCost * Time.deltaTime);
        }
    }
}
