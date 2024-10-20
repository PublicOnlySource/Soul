using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEventAction : StateAction
{
    private Animator animator;
    private float currentNormalizedTime;

    private AnimatorEventActionSO originSO => (AnimatorEventActionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        animator = stateMachine.GetComponent<Animator>();
    }


    public override void OnStateEnter()
    {
       
    }

    public override void OnStateExit()
    {
       
    }

    public override void OnUpdate()
    {
        currentNormalizedTime = animator.GetCurrentAnimatorStateInfo(originSO.LayerIndex).normalizedTime;

   
    }
}
