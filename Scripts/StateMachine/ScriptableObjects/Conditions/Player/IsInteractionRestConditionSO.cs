using UnityEngine;

[CreateAssetMenu(fileName = "IsInteractionRest", menuName = "Soul/State Machines/Conditions/IsInteractionRest")]
public class IsInteractionRestConditionSO : StateConditionSO
{
    protected override Condition CreateCondition()
    {
       return new IsInteractionRestCondition();
    }
}
