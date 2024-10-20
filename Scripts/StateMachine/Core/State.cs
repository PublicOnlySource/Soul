public class State
{
    public StateSO originSO;
    public StateTransition[] transitions;
    public StateAction[] actions;
    public StateMachine stateMachine;

    public State() { }

    public State(StateSO originSO, StateMachine stateMachine, StateAction[] actions)
    {
        this.originSO = originSO;
        this.stateMachine = stateMachine;
        this.actions = actions;
    }


    public void OnStateEnter()
    {
        OnStateEnter(transitions);
        OnStateEnter(actions);
    }

    public void OnUpdate()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].OnUpdate();
        }
    }

    public void OnStateExit()
    {
        OnStateExit(transitions);
        OnStateExit(actions);
    }

    public bool CanTransition(out State state)
    {
        state = null;

        for (int i = 0; i < transitions.Length; i++) 
        {
            if (transitions[i].CanTransition(out state))
                break;
        }

        for (int i = 0; i < transitions.Length; i++)
        {
            transitions[i].ClearConditionsCache();
        }

        return state != null;
    }

    private void OnStateEnter(IStateComponent[] stateComponents)
    {
        for (int i = 0; i < stateComponents.Length; i++)
        {
            stateComponents[i].OnStateEnter();
        }
    }

    private void OnStateExit(IStateComponent[] stateComponents)
    {
        for (int i = 0; i < stateComponents.Length; i++)
        {
            stateComponents[i].OnStateExit();
        }
    }


}
