using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterChangeAIState", menuName = "Soul/State Machines/Actions/Monster Change AIState")]

public class MonsterChangeAIStateSO : StateActionSO
{
    [SerializeField]
    private Enums.SpecificMoment whenToRun;
    [SerializeField]
    private Enums.MonsterAIState state;

    public Enums.SpecificMoment WhenToRun { get => whenToRun; }
    public Enums.MonsterAIState State { get => state; }

    protected override StateAction CreateAction()
    {
        return new MonsterChangeAIState();
    }
}
