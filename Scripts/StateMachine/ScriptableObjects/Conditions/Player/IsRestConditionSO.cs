using UnityEngine;

[CreateAssetMenu(fileName = "IsRest", menuName = "Soul/State Machines/Conditions/IsRest")]
public class IsRestConditionSO : StateConditionSO
{
    protected override Condition CreateCondition()
    {
       return new IsRestCondition();
    }
}
