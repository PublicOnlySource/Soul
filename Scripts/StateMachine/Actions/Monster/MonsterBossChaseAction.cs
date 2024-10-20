using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBossChaseAction : StateAction
{
    private MonsterAI monsterAI;
    private Coroutine coroutine;
    private Detector detector;
    private NavMeshAgent agent;
    private Transform transform;
    private Vector3 movement;
    private float distance;

    private MonsterBossChaseActionSO originSO => (MonsterBossChaseActionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        monsterAI = stateMachine.GetComponent<MonsterAI>();
        detector = stateMachine.GetComponent<Detector>();
        agent = stateMachine.GetComponent<NavMeshAgent>();
        transform = stateMachine.transform;
    }

    public override void OnStateEnter()
    {
        agent.enabled = true;
        coroutine = CoroutineUtil.Instance.StartCoroutine(Chase());
    }

    public override void OnStateExit()
    {
        CoroutineUtil.Instance.StopCoroutine(coroutine);
    }

    public override void OnUpdate()
    {
    }

    private int GetRandomNumber0To100()
    {
        return Random.Range(0, 100);
    }

    private IEnumerator Chase()
    {
        while (true)
        {
            if (!detector.DetectObject)
            {
                monsterAI.UpdateState(Enums.MonsterAIState.Idle);
                break;
            }

            distance = Vector3.Distance(transform.position, detector.DetectObject.transform.position);
 
            if (distance <= originSO.BackStepDistacne ||
                (distance < originSO.MinChangeMoveDistance && GetRandomNumber0To100() <= originSO.BackPercentage))
            {
                monsterAI.UpdateState(Enums.MonsterAIState.BackJump);
                coroutine = null;
                break;
            }
            else if (distance > originSO.MaxChangeMoveDistance)
            {
                movement = transform.forward;
                int r = GetRandomNumber0To100();
               
                if (r <= originSO.SprintPercentage)
                {
                    monsterAI.UpdateState(Enums.MonsterAIState.Sprint); 
                    coroutine = null;
                    break;
                }
            }
            else
            {
                movement = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            }

            agent.Move(movement * 50 * Time.deltaTime);
            yield return CoroutineUtil.Instance.WaitForSeconds(Random.Range(originSO.MinMoveCoolTime, originSO.MaxMoveCoolTime));
        }
    }
}