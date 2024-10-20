using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsMonsterDetect", menuName = "Soul/State Machines/Conditions/IsMonsterDetect")]

public class IsMonsterDetectConditionSO : StateConditionSO
{
    protected override Condition CreateCondition()
    {
        return new IsMonsterDetectCondition();
    }
}
