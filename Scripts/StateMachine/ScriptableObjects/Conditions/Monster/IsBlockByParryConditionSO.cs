using UnityEngine;

[CreateAssetMenu(fileName = "IsBlockByParry", menuName = "Soul/State Machines/Conditions/IsBlockByParry")]
public class IsBlockByParryConditionSO : StateConditionSO
{
    protected override Condition CreateCondition()
    {
        return new IsBlockByParryCondition();  
    }
}
