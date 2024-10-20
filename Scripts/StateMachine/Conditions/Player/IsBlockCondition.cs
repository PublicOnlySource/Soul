public class IsBlockCondition : Condition
{
    public override void Init(StateMachine stateMachine, StateConditionSO originSO)
    {
        base.Init(stateMachine, originSO);
    }

    protected override bool Statement()
    {
        return InputManager.Instance.IsBlock;
    }
}