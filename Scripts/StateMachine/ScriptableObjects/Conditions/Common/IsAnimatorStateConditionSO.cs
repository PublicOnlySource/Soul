using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsAnimatorState_", menuName = "Soul/State Machines/Conditions/IsAnimatorState")]
public class IsAnimatorStateConditionSO : StateConditionSO
{
    [SerializeField]
    private string stateName;
    [SerializeField]
    private Enums.InequalitySign inequalitySign;
    [SerializeField]
    private float targetNormalizedTime;
    [SerializeField]
    private int layerIndex = 0;


    public string StateName { get => stateName; }
    public Enums.InequalitySign InequalitySign { get => inequalitySign; }
    public float TargetNormalizedTime { get => targetNormalizedTime; }
    public int LayerIndex { get => layerIndex; }

    protected override Condition CreateCondition()
    {
        return new IsAnimatorStateCondition();
    }
}
