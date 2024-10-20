using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsMovingCondition : Condition
{
    private IsMovingConditionSO originSO => (IsMovingConditionSO)OriginSO;

    protected override bool Statement()
    {
        return InputManager.Instance.MoveAmount > originSO.treshold;
    }
}
