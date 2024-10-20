using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Soul/State Machines/Conditions/IsItemUseMotionCondition")]
public class IsItemUseMotionConditionSO : StateConditionSO
{
    [SerializeField]
    private Enums.ConsumableItemMotionType motionType;

    public Enums.ConsumableItemMotionType MotionType { get => motionType; }

    protected override Condition CreateCondition()
    {
        return new IsItemUseMotionConditon();
    }
}
