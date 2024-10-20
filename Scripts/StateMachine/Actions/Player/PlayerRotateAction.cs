using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotateAction : StateAction
{
    private PlayerController controller;
    private LockOn lockOn;
    private Vector3 targetRotationDirection;
    private Transform target;

    private PlayerRotateActionSO originSO => (PlayerRotateActionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        target = stateMachine.transform;
        controller = stateMachine.GetComponent<PlayerController>();
        lockOn = stateMachine.GetComponent<LockOn>();
    }

    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
       
    }

    public override void OnUpdate()
    {
        if (controller.IsPerformingAction && lockOn.IsEnable())
            return;

        targetRotationDirection = Vector3.zero;
        targetRotationDirection = FollowCameraComponent.Instance.CameraObject.transform.forward * InputManager.Instance.VerticalInput;
        targetRotationDirection += FollowCameraComponent.Instance.CameraObject.transform.right * InputManager.Instance.HorizontalInput;

        targetRotationDirection.Normalize();
        targetRotationDirection.y = 0;

        if (targetRotationDirection == Vector3.zero)
        {
            targetRotationDirection = target.forward;
        }

        Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
        Quaternion targetRotation = Quaternion.Slerp(target.rotation, newRotation, originSO.RotateSpeed * Time.deltaTime);
        target.rotation = targetRotation;
    }
}
