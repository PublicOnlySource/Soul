using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Soul/State Machines/Conditions/IsMoving")]
public class IsMovingConditionSO : StateConditionSO
{
    public float treshold = 0.02f;

    protected override Condition CreateCondition()
    {
      return new IsMovingCondition();
    }
}
