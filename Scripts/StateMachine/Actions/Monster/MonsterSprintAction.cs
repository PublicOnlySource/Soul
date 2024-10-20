using UnityEngine;

public class MonsterSprintAction : StateAction
{
    private Animator animator;
    private Detector detector;
    private float distance;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        animator = stateMachine.GetComponent<Animator>();
        detector = stateMachine.GetComponent<Detector>();
    }

    public override void OnStateEnter()
    {
        distance = Vector3.Distance(animator.transform.position, detector.DetectObject.transform.position);

    }

    public override void OnStateExit()
    {
        
    }

    public override void OnUpdate()
    {
        
    }
}