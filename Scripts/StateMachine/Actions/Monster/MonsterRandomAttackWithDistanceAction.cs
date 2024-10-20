using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterRandomAttackWithDistanceAction : StateAction
{
    private MonsterAI monsterAI;
    private Detector detector;
    private Transform transform;
    private Coroutine coroutine;

    private MonsterRandomAttackWithDistanceActionSO originSO => (MonsterRandomAttackWithDistanceActionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        monsterAI = stateMachine.GetComponent<MonsterAI>();
        detector = stateMachine.GetComponent<Detector>();
        transform = stateMachine.transform;
    }

    public override void OnStateEnter()
    {
        coroutine = CoroutineUtil.Instance.StartCoroutine(Process());
    }

    public override void OnStateExit()
    {
        if (coroutine != null)
        {
            CoroutineUtil.Instance.StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    public override void OnUpdate()
    {
        
    }

    private IEnumerator Process()
    {
        yield return new WaitUntil(() => detector.DetectObject);
        yield return new WaitUntil(() =>  Vector3.Distance(transform.position, detector.DetectObject.transform.position) <= originSO.AttackDistance);

        coroutine = null;

        int index = Random.Range(0, originSO.AttackList.Count);
        monsterAI.UpdateState(originSO.AttackList[index]);
    }
}