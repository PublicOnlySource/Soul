using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreateConsumableItemAction : StateAction
{
    private PlayerController controller;
    private GameObject createdObj;

    private PlayerCreateConsumableItemActionSO originSO => (PlayerCreateConsumableItemActionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        controller = stateMachine.GetComponent<PlayerController>();
    }

    public override void OnStateEnter()
    {
        controller.PlayerEquitment.HideWeapon();

        ConsumableItemOffset item = PoolingManager.Instance.Pop<ConsumableItemOffset>(Constants.Item.CONSUMABLE_POTION.ToString(), true);
        createdObj = item.gameObject;
        createdObj.transform.SetParent(controller.HandRoot);
        createdObj.transform.localPosition = Vector3.zero + item.OffsetPos;
        createdObj.transform.localRotation = Quaternion.identity;
        createdObj.transform.localScale = Vector3.one + item.OffsetScale;
    }

    public override void OnStateExit()
    {
        GameObject.Destroy(createdObj);
        controller.PlayerEquitment.ShowWeapon();
    }

    public override void OnUpdate()
    {
    }
}
