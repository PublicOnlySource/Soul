public class PlayerStandUpAction : StateAction
{
    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
    }

    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
        InputManager.Instance.EnableInput();
    }

    public override void OnUpdate()
    {
        
    }
}