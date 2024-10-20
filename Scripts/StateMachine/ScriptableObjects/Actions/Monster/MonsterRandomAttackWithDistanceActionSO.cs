using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster_RandomAttackWithDistanceAction", menuName = "Soul/State Machines/Actions/MonsterRandomAttackWithDistanceAction")]
public class MonsterRandomAttackWithDistanceActionSO : StateActionSO
{
    [SerializeField]
    private float attackDistance;
    [SerializeField]
    private List<Enums.MonsterAIState> attackList;

    public float AttackDistance { get => attackDistance; }
    public List<Enums.MonsterAIState> AttackList { get => attackList; }

    protected override StateAction CreateAction()
    {
        return new MonsterRandomAttackWithDistanceAction();
    }
}