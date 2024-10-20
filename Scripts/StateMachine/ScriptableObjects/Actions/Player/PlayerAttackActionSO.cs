using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttackAction", menuName = "Soul/State Machines/Actions/PlayerAttackAction")]
public class PlayerAttackActionSO : StateActionSO
{
    protected override StateAction CreateAction()
    {
        return new PlayerAttackAction();
    }
}
