using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBlock", menuName = "Soul/State Machines/Actions/PlayerBlock")]
public class PlayerBlockActionSO : StateActionSO
{
    protected override StateAction CreateAction()
    {
        return new PlayerBlockAction();   
    }
}