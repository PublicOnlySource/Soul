using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "IsDead", menuName = "Soul/State Machines/Conditions/IsDead")]
public class IsDeadConditionSO : StateConditionSO
{
    protected override Condition CreateCondition()
    {
       return new IsDeadCondition();
    }
}
