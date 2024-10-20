using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConsumableItemMotionAction : StateAction
{
    private PlayerController controller;
    private PlayerConsumableItemMotionActionSO originSO => (PlayerConsumableItemMotionActionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        controller = stateMachine.GetComponent<PlayerController>();
    }

    public override void OnStateEnter()
    {
        if (originSO.WhenToRun == Enums.SpecificMoment.OnStateEnter)
        {
            EventManager.Instance.TriggerEvent(Enums.EventType.Player_Enums_consumableItemMotionType_SetConsumableItemMotion, originSO.MotionType);
        }
    }

    public override void OnStateExit()
    {
        if (originSO.WhenToRun == Enums.SpecificMoment.OnStateExit)
        {
            EventManager.Instance.TriggerEvent(Enums.EventType.Player_Enums_consumableItemMotionType_SetConsumableItemMotion, originSO.MotionType);
        }
    }

    public override void OnUpdate()
    {
    }
}
