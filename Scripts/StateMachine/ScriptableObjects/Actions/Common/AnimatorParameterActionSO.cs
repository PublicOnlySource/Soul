using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimatorParameterAction", menuName = "Soul/State Machines/Actions/Animator Parameter")]
public class AnimatorParameterActionSO : StateActionSO
{
    [SerializeField]
    private Enums.SpecificMoment whenToRun;
    [SerializeField]
    private string parameterName;
    [SerializeField]
    private Enums.ParameterType parameterType;
    [SerializeField]
    private bool boolValue;
    [SerializeField]
    private int intValue;
    [SerializeField]
    private float floatValue;


    public Enums.SpecificMoment WhenToRun { get => whenToRun; }
    public Enums.ParameterType ParameterType { get => parameterType; }
    public bool BoolValue { get => boolValue; }
    public int IntValue { get => intValue; }
    public float FloatValue { get => floatValue; }

    protected override StateAction CreateAction()
    {
        return new AnimatorParameterAction(parameterName);
    }
}
