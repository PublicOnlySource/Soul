using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsMonsterDistance", menuName = "Soul/State Machines/Conditions/IsMonsterDistance")]

public class IsMonsterDistanceConditionSO : StateConditionSO
{
    [SerializeField]
    private Enums.InequalitySign inequalitySign;
    [SerializeField]
    private float targetDistance;

    public Enums.InequalitySign InequalitySign { get => inequalitySign; }
    public float TargetDistance { get => targetDistance; }

    protected override Condition CreateCondition()
    {
        return new IsMonsterDistanceCondition();
    }
}
