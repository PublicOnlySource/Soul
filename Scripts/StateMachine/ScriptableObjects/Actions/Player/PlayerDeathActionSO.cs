using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDeathAction", menuName = "Soul/State Machines/Actions/PlayerDeathAction")]
public class PlayerDeathActionSO : StateActionSO
{
    protected override StateAction CreateAction()
    {
        return new PlayerDeathAction();
    }
}