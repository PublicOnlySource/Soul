using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsHit", menuName = "Soul/State Machines/Conditions/IsHit")]

public class IsHitConditionSO : StateConditionSO
{
    protected override Condition CreateCondition()
    {
       return new IsHitCondition();
    }
}
