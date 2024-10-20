using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorParameterAction : StateAction
{
    private string parameterName;
    private Animator animator;
    private AnimatorParameterActionSO originSO => (AnimatorParameterActionSO)base.OriginSO;

    public AnimatorParameterAction(string parameterName)
    {
        this.parameterName = parameterName;
    }

    public override void OnUpdate()
    {

    }

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        animator = stateMachine.GetComponent<Animator>();        
    }

    public override void OnStateEnter()
    {
        if (originSO.WhenToRun == Enums.SpecificMoment.OnStateEnter)
            SetParameter();
    }

    public override void OnStateExit()
    {
        if (originSO.WhenToRun == Enums.SpecificMoment.OnStateExit)
            SetParameter();
    }


    private void SetParameter()
    {
        switch (originSO.ParameterType)
        {
            case Enums.ParameterType.Float:
                animator.SetFloat(parameterName, originSO.FloatValue);
                break;
            case Enums.ParameterType.Int:
                animator.SetInteger(parameterName, originSO.IntValue);
                break;
            case Enums.ParameterType.Bool:
                animator.SetBool(parameterName, originSO.BoolValue);
                break;
            case Enums.ParameterType.Trigger:
                animator.SetTrigger(parameterName);
                break;
        }
    }

  
}
