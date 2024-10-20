using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsMonsterDetectCondition : Condition
{
    private Detector detector;

    public override void Init(StateMachine stateMachine, StateConditionSO originSO)
    {
        base.Init(stateMachine, originSO);
        detector = stateMachine.GetComponent<Detector>();
    }

    protected override bool Statement()
    {
        return detector.IsDetect;
    }
}
