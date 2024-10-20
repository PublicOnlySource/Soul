public class IsBlockByParryCondition : Condition
{
    private IUseBlockByParry blockByParry;

    public override void Init(StateMachine stateMachine, StateConditionSO originSO)
    {
        base.Init(stateMachine, originSO);
        blockByParry= stateMachine.GetComponent<IUseBlockByParry>();
    }

    protected override bool Statement()
    {
        return blockByParry.IsBlockByParry;
    }
}