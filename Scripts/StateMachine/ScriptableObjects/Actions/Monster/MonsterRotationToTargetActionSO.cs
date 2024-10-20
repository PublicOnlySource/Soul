using UnityEngine;

[CreateAssetMenu(fileName = "Monster_RotationToTargetAction", menuName = "Soul/State Machines/Actions/MonsterRotationToTargetAction")]
public class MonsterRotationToTargetActionSO : StateActionSO
{
    [SerializeField]
    private float rotationSpeed;

    public float RotationSpeed { get => rotationSpeed; }

    protected override StateAction CreateAction()
    {
        return new MonsterRotationToTargetAction();
    }
}