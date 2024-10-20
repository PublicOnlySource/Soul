using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterPatrolAction", menuName = "Soul/State Machines/Actions/MonsterPatrolAction")]
public class MonsterPatrolActionSO : StateActionSO
{
    protected override StateAction CreateAction()
    {
        return new MonsterPatrolAction();
    }
}
