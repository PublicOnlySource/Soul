public class IsLockOnCondition : Condition
{
    private LockOn lockOn;

    public override void Init(StateMachine stateMachine, StateConditionSO originSO)
    {
        base.Init(stateMachine, originSO);
        lockOn = stateMachine.GetComponent<LockOn>();
    }

    protected override bool Statement()
    {
        return lockOn.IsEnable();
    }
}