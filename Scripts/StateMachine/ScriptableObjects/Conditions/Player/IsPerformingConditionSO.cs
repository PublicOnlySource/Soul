using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsPerforming", menuName = "Soul/State Machines/Conditions/IsPerforming")]
public class IsPerformingConditionSO : StateConditionSO
{        
    protected override Condition CreateCondition()
    {
        return new IsPerformingConditon();
    }
}