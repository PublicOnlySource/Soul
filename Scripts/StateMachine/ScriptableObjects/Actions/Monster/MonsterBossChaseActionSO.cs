using UnityEngine;

[CreateAssetMenu(fileName = "MonsterBossChaseAction", menuName = "Soul/State Machines/Actions/MonsterBossChaseAction")]
public class MonsterBossChaseActionSO : StateActionSO
{
    [SerializeField]
    private float sprintPercentage = 30;
    [SerializeField]
    private float backPercentage = 30;
    [SerializeField]
    private float minMoveCoolTime = 1;
    [SerializeField]
    private float maxMoveCoolTime = 4;
    [SerializeField]
    private float minChangeMoveDistance = 4f;
    [SerializeField]
    private float maxChangeMoveDistance = 10f;
    [SerializeField]
    private float backStepDistance = 2f;

    public float SprintPercentage { get => sprintPercentage; }
    public float MinMoveCoolTime { get => minMoveCoolTime; }
    public float MaxMoveCoolTime { get => maxMoveCoolTime; }
    public float MinChangeMoveDistance { get => minChangeMoveDistance; }
    public float MaxChangeMoveDistance { get => maxChangeMoveDistance; }
    public float BackPercentage { get => backPercentage; }
    public float BackStepDistacne { get => backStepDistance; }

    protected override StateAction CreateAction()
    {
        return new MonsterBossChaseAction();
    }
}