
public class StateCondition
{
    private StateMachine stateMachine;
    private Condition condition;
    private bool expectedResult;
    public Condition Condition { get => condition; }


    public StateCondition(StateMachine stateMachine, Condition condition, bool expectedResult)
    {
        this.stateMachine = stateMachine;
        this.condition = condition;
        this.expectedResult = expectedResult;
    }


    public bool IsVerify()
    {
        bool statement = condition.GetStatement();
        bool isVerify = statement == expectedResult;

        return isVerify;
    }
}
