using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementAction", menuName = "Soul/State Machines/Actions/PlayerMovement")]
public class PlayerMovementActionSO : StateActionSO
{
    [SerializeField]
    private PlayerActionConfigSO playerActionConfigSO;

    public PlayerActionConfigSO PlayerActionConfigSO { get => playerActionConfigSO; }

    protected override StateAction CreateAction()
    {
        return new PlayerMovementAciton();
    }
}
