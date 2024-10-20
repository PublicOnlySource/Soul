using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster_AttackType1Action", menuName = "Soul/State Machines/Actions/MonsterAttackType1Action")]
public class MonsterAttackType1ActionSO : StateActionSO
{
    protected override StateAction CreateAction()
    {
        return new MonsterAttackType1Action();
    }
}
