using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster_DeathAction", menuName = "Soul/State Machines/Actions/MonsterDeathAction")]

public class MonsterDeathActionSO : StateActionSO
{
    [SerializeField]
    private Enums.MonsterType monsterType;
    [SerializeField]
    private int giveSoulValue;

    public int GiveSoulValue { get => giveSoulValue; }
    public Enums.MonsterType MonsterType { get => monsterType; }

    protected override StateAction CreateAction()
    {
        return new MonsterDeathAction();
    }
}
