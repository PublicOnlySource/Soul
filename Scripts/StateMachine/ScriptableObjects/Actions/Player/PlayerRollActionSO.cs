using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerRollAction", menuName = "Soul/State Machines/Actions/PlayerRollAction")]
public class PlayerRollActionSO : StateActionSO
{
    [SerializeField]
    private PlayerActionConfigSO playerActionConfigSO;

    public PlayerActionConfigSO PlayerActionConfigSO { get => playerActionConfigSO; }

    protected override StateAction CreateAction()
    {
       return new PlayerRollAction();
    }
}
