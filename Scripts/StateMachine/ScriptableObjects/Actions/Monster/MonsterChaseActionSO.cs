using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterChaseAction", menuName = "Soul/State Machines/Actions/MonsterChaseAction")]
public class MonsterChaseActionSO : StateActionSO
{
    [SerializeField]
    private float attackDistance;

    public float AttackDistance { get => attackDistance; }

    protected override StateAction CreateAction()
    {
        return new MonsterChaseAction();
    }
}
