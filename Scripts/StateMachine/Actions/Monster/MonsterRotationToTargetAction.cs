using System.Collections;
using UnityEngine;
using static Constants;
using static UnityEngine.UI.Image;

public class MonsterRotationToTargetAction : StateAction
{
    private Transform transform;
    private Detector detector;
    private Vector3 directionToPlayer;
    private Quaternion lookPlayerRotation;
    private Coroutine coroutine;

    private MonsterRotationToTargetActionSO originSO => (MonsterRotationToTargetActionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        transform = stateMachine.transform;
        detector = stateMachine.GetComponent<Detector>();
    }

    public override void OnStateEnter()
    {
        coroutine = CoroutineUtil.Instance.StartCoroutine(Process());
    }

    public override void OnStateExit()
    {
        CoroutineUtil.Instance.StopCoroutine(coroutine);
    }

    public override void OnUpdate()
    {        
        
    }

    private IEnumerator Process()
    {
        while(true)
        {
            yield return new WaitUntil(() => detector.DetectObject);
            directionToPlayer = (detector.DetectObject.transform.position - transform.position).normalized;
            lookPlayerRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookPlayerRotation, Time.deltaTime * originSO.RotationSpeed);
        }
    }
}