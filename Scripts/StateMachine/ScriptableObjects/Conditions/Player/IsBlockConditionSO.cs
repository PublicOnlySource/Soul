using UnityEngine;

[CreateAssetMenu(fileName = "IsBlock", menuName = "Soul/State Machines/Conditions/IsBlock")]
public class IsBlockConditionSO : StateConditionSO
{
    protected override Condition CreateCondition()
    {
       return new IsBlockCondition();
    }
}
