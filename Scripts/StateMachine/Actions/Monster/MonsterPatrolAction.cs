using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEngine;
using UnityEngine.AI;

public class MonsterPatrolAction : StateAction
{
    private MonsterAI monsterAI;
    private MonsterController monsterController;
    private Coroutine coroutine;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        monsterAI = stateMachine.GetComponent<MonsterAI>();
        monsterController = stateMachine.GetComponent<MonsterController>();
    }

    public override void OnStateEnter()
    {
        monsterAI.Detector.Clear();
        monsterAI.Detector.EnableDetect();
        coroutine = CoroutineUtil.Instance.StartCoroutine(Patrol());
    }

    public override void OnStateExit()
    {
        monsterAI.Detector.DisableDetect();
        CoroutineUtil.Instance.StopCoroutine(coroutine);
    }

    public override void OnUpdate() { }

    IEnumerator Patrol()
    {
        monsterAI.EnableAgent();

        while (true)
        {
            yield return new WaitUntil(() => SetRandomNextPos());

            yield return new WaitUntil(() =>
            {
                if (monsterAI.AgentRemainingDistance == 0)
                {
                    int rnd = Random.Range(1, 101);

                    if (rnd <= 50) monsterAI.UpdateState(Enums.MonsterAIState.Idle);
                    return true;
                }
                return false;
            });

            yield return CoroutineUtil.Instance.WaitForSeconds(CoroutineUtil.UPDATE_SECOND_0_01);
        }
    }

    private bool SetRandomNextPos()
    {
        if (IsOverDistanceFromOriginPos())
            return true;

        if (monsterAI.GetRandomPosition(out Vector3 result))
        {
            monsterAI.Move(result);
            return true;
        }

        return false;
    }

    private bool IsOverDistanceFromOriginPos()
    {
        if (Vector3.Distance(monsterController.CreatedPos, monsterController.transform.position) > monsterController.ReturnDistance)
        {
            monsterAI.Move(monsterController.CreatedPos);
            return true;
        }

        return false;
    }
}
