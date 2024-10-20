using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsPerforming", menuName = "Soul/State Machines/Actions/IsPerforming")]
public class IsPerformingActionSO : StateActionSO
{
    [SerializeField]
    private Enums.SpecificMoment whenToRun;
    [SerializeField]
    private bool boolValue;

    public Enums.SpecificMoment WhenToRun { get => whenToRun; }
    public bool BoolValue { get => boolValue; }

    protected override StateAction CreateAction()
    {
        return new IsPerformingAction();
    }
}
