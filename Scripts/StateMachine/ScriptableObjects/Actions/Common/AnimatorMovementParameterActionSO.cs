using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimatorVerticalParameterAction", menuName = "Soul/State Machines/Actions/Animator Vertical Parameter")]
public class AnimatorMovementParameterActionSO : StateActionSO
{
    [SerializeField]
    private Enums.SpecificMoment whenToRun;
    [SerializeField]
    private bool horizontal = false;
    [SerializeField] 
    private bool vertical = false;
    [SerializeField]
    private bool isLockOn = false;


    public Enums.SpecificMoment WhenToRun { get => whenToRun; }
    public bool IsLockOn { get => isLockOn; }
    public bool Horizontal { get => horizontal; }
    public bool Vertical { get => vertical; }

    protected override StateAction CreateAction()
    {
        return new AnimatorMovementParameterWithInputAction();
    }
}
