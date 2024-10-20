public class IsRestCondition : Condition
{
    private PlayerController controller;

    public override void Init(StateMachine stateMachine, StateConditionSO originSO)
    {
        base.Init(stateMachine, originSO);
        controller = stateMachine.GetComponent<PlayerController>();
    }

    protected override bool Statement()
    {
        return controller.IsRest;
    }
}