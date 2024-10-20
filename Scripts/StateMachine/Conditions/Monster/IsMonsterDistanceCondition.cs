using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsMonsterDistanceCondition : Condition
{
    private MonsterAI monsterAI;
    private Detector detector;
    private float distance;

    private IsMonsterDistanceConditionSO originSO => (IsMonsterDistanceConditionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateConditionSO originSO)
    {
        base.Init(stateMachine, originSO);
        monsterAI = stateMachine.GetComponent<MonsterAI>();
        detector = stateMachine.GetComponent<Detector>();
    }

    protected override bool Statement()
    {
        if (detector.DetectObject == null)
            return false;

        distance = Vector3.Distance(monsterAI.transform.position, detector.DetectObject.transform.position);

        switch (originSO.InequalitySign)
        {
            case Enums.InequalitySign.LessThan:
                if (distance < originSO.TargetDistance)
                    return true;
                break;
            case Enums.InequalitySign.LessThanOrEqual:
                if (distance <= originSO.TargetDistance)
                    return true;
                break;
            case Enums.InequalitySign.GreaterThan:
                if (distance > originSO.TargetDistance)
                    return true;
                break;
            case Enums.InequalitySign.GreaterThanOrEqual:
                if (distance >= originSO.TargetDistance)
                    return true;
                break;
            case Enums.InequalitySign.Equal:
                if (distance == originSO.TargetDistance)
                    return true;
                break;
        }
       
        return false;
    }
}
