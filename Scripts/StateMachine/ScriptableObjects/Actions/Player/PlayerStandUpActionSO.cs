using UnityEngine;

[CreateAssetMenu(fileName = "Player_StandUpAction", menuName = "Soul/State Machines/Actions/PlayerStandUp")]
public class PlayerStandUpActionSO : StateActionSO
{
    protected override StateAction CreateAction()
    {
        return new PlayerStandUpAction();
    }
}