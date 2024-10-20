using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerRollAction : StateAction
{
    private PlayerController controller;
    private LockOn lockOn;

    private PlayerRollActionSO originSO => (PlayerRollActionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        lockOn = stateMachine.GetComponent<LockOn>();
        controller = stateMachine.GetComponent<PlayerController>();
    }

    public override void OnStateEnter()
    {
        controller.PlayerStamina.Use(originSO.PlayerActionConfigSO.RollCost);

        if (lockOn.IsEnable())
        {
            lockOn.IsRotateEnable = false;
            Vector3 inputDirection = new Vector3(InputManager.Instance.HorizontalInput, 0, InputManager.Instance.VerticalInput).normalized;
            Vector3 lockOnTargetDirection = lockOn.GetDirection();
            lockOnTargetDirection.y = 0;

            if (inputDirection == Vector3.back)
            {
                lockOn.transform.rotation = Quaternion.LookRotation(-lockOnTargetDirection);
            }
            else if (inputDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lockOnTargetDirection);
                Quaternion inputRotation = Quaternion.FromToRotation(Vector3.forward, inputDirection);
                lockOn.transform.rotation = targetRotation * inputRotation;
            }
        }
    }

    public override void OnStateExit()
    {
        if (lockOn.IsEnable())
        {
            lockOn.IsRotateEnable = true;
        }
    }

    public override void OnUpdate()
    {

    }
}
