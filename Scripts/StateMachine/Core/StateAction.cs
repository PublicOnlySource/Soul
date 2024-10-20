
public abstract class StateAction : IStateComponent
{
    private StateActionSO originSO;
    protected StateActionSO OriginSO { get => originSO; }


    public abstract void OnUpdate();
    public abstract void OnStateEnter();
    public abstract void OnStateExit();

    public virtual void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        originSO = stateActionSO;
    }
}
