public class IsAttackCondition : Condition
{
    private PlayerController controller;

    private IsAttackConditionSO originSO => (IsAttackConditionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateConditionSO originSO)
    {
        base.Init(stateMachine, originSO);
        controller = stateMachine.GetComponent<PlayerController>();
    }

    protected override bool Statement()
    {
        if (controller.PlayerStamina.Stamina < controller.PlayerEquitment.ItemData?.Stamina)
            return false;

        if (originSO.WithoutIsPerformingAction)
        {
            return InputManager.Instance.IsAttack;
        }

        return InputManager.Instance.IsAttack && !controller.IsPerformingAction;
    }
}
