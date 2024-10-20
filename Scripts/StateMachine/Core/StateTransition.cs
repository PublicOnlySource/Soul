
public class StateTransition : IStateComponent
{
    private State targetState;
    private StateCondition[] conditions;
    private int[] resultGroups;
    private bool[] results;

    public StateTransition() { }

    public StateTransition(State targetState, StateCondition[] conditions, int[] resultGroups = null)
    {
        Init(targetState, conditions, resultGroups);
    }

    internal void Init(State targetState, StateCondition[] conditions, int[] resultGroups = null)
    {
        this.targetState = targetState;
        this.conditions = conditions;
        this.resultGroups = resultGroups != null && resultGroups.Length > 0 ? resultGroups : new int[1];
        this.results = new bool[this.resultGroups.Length];
    }

    public bool CanTransition(out State state)
    {
        state = IsVerifyTransition() ? targetState : null;
        return state != null;
    }


    public void OnStateEnter()
    {
        for (int i = 0; i < conditions.Length; i++)
        {
            conditions[i].Condition.OnStateEnter();
        }
    }

    public void OnStateExit()
    {
        for (int i = 0; i < conditions.Length; i++)
        {
            conditions[i].Condition.OnStateExit();
        }
    }

    public void ClearConditionsCache()
    {
        for (int i = 0; i < conditions.Length; i++)
        {
            conditions[i].Condition.ClearCache();
        }
    }

    private bool IsVerifyTransition()
    {
        int count = resultGroups.Length;
        for (int i = 0, idx = 0; i < count && idx < conditions.Length; i++)
        {
            for (int j = 0; j < resultGroups[i]; j++, idx++)
            {
                results[i] = j == 0 ? conditions[idx].IsVerify() : results[i] && conditions[idx].IsVerify();
            }
        }

        bool result = false;
        for (int i = 0; i < count && !result; i++)
        {
            result = result || results[i];
        }

        return result;
    }


}

