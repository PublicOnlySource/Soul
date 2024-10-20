using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockByParryAction", menuName = "Soul/State Machines/Actions/BlockByParryAction")]
public class BlockByParryActionSO : StateActionSO
{
    protected override StateAction CreateAction()
    {
        return new BlockByParryAction();
    }
}
