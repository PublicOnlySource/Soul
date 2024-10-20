using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsMonsterAIState", menuName = "Soul/State Machines/Conditions/IsMonsterAIState")]
public class IsMonsterAIStateConditionSO : StateConditionSO
{
    [SerializeField]
    private Enums.MonsterAIState targetState;

    public Enums.MonsterAIState TargetState { get => targetState; }

    protected override Condition CreateCondition()
    {
        return new IsMonsterAIStateCondition();
    }
}
