using UnityEngine;

[CreateAssetMenu(fileName = "IsLockOnCondition", menuName = "Soul/State Machines/Conditions/IsLockOn")]
public class IsLockOnConditionSO : StateConditionSO
{
    protected override Condition CreateCondition()
    {
       return new IsLockOnCondition();
    }
}
