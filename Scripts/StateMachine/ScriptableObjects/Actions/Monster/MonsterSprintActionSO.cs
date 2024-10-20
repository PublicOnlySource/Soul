using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSprintAction", menuName = "Soul/State Machines/Actions/MonsterSprintAction")]
public class MonsterSprintActionSO : StateActionSO
{
    protected override StateAction CreateAction()
    {
        return new MonsterSprintAction();
    }
}