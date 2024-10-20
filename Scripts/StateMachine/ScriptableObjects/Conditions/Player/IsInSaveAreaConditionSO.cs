using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Soul/State Machines/Conditions/IsInSaveArea")]
public class IsInSaveAreaConditionSO : StateConditionSO
{
    protected override Condition CreateCondition()
    {
       return new IsInSaveAreaCondition();
    }
}
