using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCreateConsumableItemAction", menuName = "Soul/State Machines/Actions/Player Create ConsumableItem Action")]
public class PlayerCreateConsumableItemActionSO : StateActionSO
{
    protected override StateAction CreateAction()
    {
       return new PlayerCreateConsumableItemAction();
    }
}
