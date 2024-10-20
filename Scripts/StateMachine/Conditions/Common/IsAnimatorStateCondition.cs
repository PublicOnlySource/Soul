using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEngine;

public class IsAnimatorStateCondition : Condition
{
    private Animator animator;
    private float currentNormalizedTime = 0f;

    private IsAnimatorStateConditionSO originSO => (IsAnimatorStateConditionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateConditionSO originSO)
    {
        base.Init(stateMachine, originSO);
        animator = stateMachine.GetComponent<Animator>();
    }

    protected override bool Statement()
    {
        if (animator.GetCurrentAnimatorStateInfo(originSO.LayerIndex).IsName(originSO.StateName))
        {
            currentNormalizedTime = animator.GetCurrentAnimatorStateInfo(originSO.LayerIndex).normalizedTime;
            
            
            switch (originSO.InequalitySign)
            {
                case Enums.InequalitySign.LessThan:
                    if (currentNormalizedTime < originSO.TargetNormalizedTime)
                        return true;
                    break;
                case Enums.InequalitySign.LessThanOrEqual:
                    if (currentNormalizedTime <= originSO.TargetNormalizedTime)
                        return true;
                    break;
                case Enums.InequalitySign.GreaterThan:
                    if (currentNormalizedTime > originSO.TargetNormalizedTime)
                        return true;
                    break;
                case Enums.InequalitySign.GreaterThanOrEqual:
                    if (currentNormalizedTime >= originSO.TargetNormalizedTime)
                        return true;
                    break;
                case Enums.InequalitySign.Equal:
                    if (currentNormalizedTime == originSO.TargetNormalizedTime)
                        return true;
                    break;
            }
        }

        return false;
    }
}
