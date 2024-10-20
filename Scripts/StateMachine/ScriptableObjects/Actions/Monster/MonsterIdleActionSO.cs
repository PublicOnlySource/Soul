using UnityEngine;

[CreateAssetMenu(fileName = "MonsterIdleAction", menuName = "Soul/State Machines/Actions/MonsterIdleAction")]
public class MonsterIdleActionSO : StateActionSO
{
    protected override StateAction CreateAction()
    {
        return new MonsterIdleAction();
    }
}