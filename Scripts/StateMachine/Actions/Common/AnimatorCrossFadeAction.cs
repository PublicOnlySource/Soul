using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorCrossFadeAction : StateAction
{
    private Animator animator;
    private AnimatorCrossFadeActionSO originSO => (AnimatorCrossFadeActionSO)OriginSO;


    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        animator = stateMachine.GetComponent<Animator>();
    }

    public override void OnStateEnter()
    {
        if (originSO.WhenToRun == Enums.SpecificMoment.OnStateEnter)
            animator.CrossFade(originSO.AnimationName, originSO.NormalizedTransitionDuration, originSO.LayerIndex, 0.01f);
    }

    public override void OnStateExit()
    {
        if (originSO.WhenToRun == Enums.SpecificMoment.OnStateExit)
            animator.CrossFade(originSO.AnimationName, originSO.NormalizedTransitionDuration, originSO.LayerIndex, 0.01f);
    }

    public override void OnUpdate()
    {
    }
}
