using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Soul/State Machines/Conditions/IsRoll")]
public class IsRollConditionSO : StateConditionSO
{
    [SerializeField]
    private PlayerActionConfigSO playerActionConfigSO;

    public PlayerActionConfigSO PlayerActionConfigSO { get => playerActionConfigSO; }

    protected override Condition CreateCondition()
    {
        return new IsRollCondition();
    }
}
