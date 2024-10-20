using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConsumableItemMotionAction", menuName = "Soul/State Machines/Actions/Player ConsumableItem Motion Action")]

public class PlayerConsumableItemMotionActionSO : StateActionSO
{
    [SerializeField]
    private Enums.SpecificMoment whenToRun;
    [SerializeField]
    private Enums.ConsumableItemMotionType motionType;

    public Enums.SpecificMoment WhenToRun { get => whenToRun; }
    public Enums.ConsumableItemMotionType MotionType { get => motionType; }

    protected override StateAction CreateAction()
    {
        return new PlayerConsumableItemMotionAction();
    }
}
