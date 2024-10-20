using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Soul/State Machines/Conditions/IsAttack")]
public class IsAttackConditionSO : StateConditionSO
{
    [SerializeField]
    private bool withoutIsPerformingAction;

    public bool WithoutIsPerformingAction { get => withoutIsPerformingAction; }

    protected override Condition CreateCondition()
    {
        return new IsAttackCondition();
    }
}
