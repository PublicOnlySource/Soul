public class MonsterSetDetecetAction : StateAction
{
    private MonsterAI monsterAI;

    private MonsterSetDetectActionSO originSO => (MonsterSetDetectActionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        monsterAI = stateMachine.GetComponent<MonsterAI>();
    }

    public override void OnStateEnter()
    {
        if (originSO.WhenToRun == Enums.SpecificMoment.OnStateEnter)
        {
            ProcessUseClear();
            ProcessState();
        }
    }

    public override void OnStateExit()
    {
        if (originSO.WhenToRun == Enums.SpecificMoment.OnStateExit)
        {
            ProcessUseClear();
            ProcessState();
        }
    }

    public override void OnUpdate()
    {
        
    }

    private void ProcessState()
    {
        if (originSO.Enable)
        {
            monsterAI.Detector.EnableDetect();
        }
        else
        {
            monsterAI.Detector.DisableDetect();
        }
    }

    private void ProcessUseClear()
    {
        if (!originSO.UseClear)
            return;

        monsterAI.Detector.Clear();
    }
}