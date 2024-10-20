using UnityEngine;

public class PlayerBlockAction : StateAction
{
    private Parry parry;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        parry = stateMachine.GetComponent<Parry>();
    }

    public override void OnStateEnter()
    {
        parry.EnableParry();
    }

    public override void OnStateExit()
    {
        parry.DisableParry();
    }

    public override void OnUpdate()
    {

    }
}