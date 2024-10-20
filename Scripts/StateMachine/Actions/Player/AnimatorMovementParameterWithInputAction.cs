using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorMovementParameterWithInputAction : StateAction
{
    private Animator animator;

    private float horizontal;
    private float vertical;

    private AnimatorMovementParameterActionSO originSO => (AnimatorMovementParameterActionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        animator = stateMachine.GetComponent<Animator>();
    }

    public override void OnStateEnter()
    {
        if (originSO.WhenToRun == Enums.SpecificMoment.OnStateEnter)
            Process();
    }

    public override void OnStateExit()
    {
        if (originSO.WhenToRun == Enums.SpecificMoment.OnStateExit)
            Process();
    }

    public override void OnUpdate()
    {
        if (originSO.WhenToRun == Enums.SpecificMoment.OnUpdate)
            Process();
    }

    private void Process()
    {
        if (originSO.Vertical)
        {
            if (originSO.IsLockOn) vertical = InputManager.Instance.VerticalInput;
            else vertical = InputManager.Instance.MoveAmount;
        }

        if (originSO.Horizontal)
        {
            if (originSO.IsLockOn) horizontal = InputManager.Instance.HorizontalInput;
            else horizontal = InputManager.Instance.MoveAmount;
        }


        animator.SetFloat(Constants.Animation.NAME_VERTICAL, vertical, 0.1f, Time.deltaTime);
        animator.SetFloat(Constants.Animation.NAME_HORIZONTAL, horizontal, 0.1f, Time.deltaTime);
    }
}
