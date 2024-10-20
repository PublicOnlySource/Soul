using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChaseAction : StateAction
{
    private MonsterAI monsterAI;
    private MonsterController monsterController;
    private Coroutine coroutine;

    private MonsterChaseActionSO originSO => (MonsterChaseActionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        monsterAI = stateMachine.GetComponent<MonsterAI>();
        monsterController = stateMachine.GetComponent<MonsterController>();
    }

    public override void OnStateEnter()
    {
        monsterAI.UpdateState(Enums.MonsterAIState.Chase);
        coroutine = CoroutineUtil.Instance.StartCoroutine(Chase());
    }

    public override void OnStateExit()
    {
        CoroutineUtil.Instance.StopCoroutine(coroutine);       
    }

    public override void OnUpdate()
    {
    }

    private IEnumerator Chase()
    {
        Transform target = monsterAI.Detector.DetectObject.transform;
        float distance = 0f;
        int random = 0;

        if (target == null)
        {
            monsterAI.UpdateState(Enums.MonsterAIState.Patrol);
            coroutine = null;
            yield break;
        }

        monsterAI.EnableAgent();

        while (true)
        {
            distance = Vector3.Distance(target.position, monsterAI.transform.position);
            
            if (distance < originSO.AttackDistance)
            {
                random = Random.Range(0, 100);

                monsterAI.UpdateState(random < 50 ? Enums.MonsterAIState.AttackType1 : Enums.MonsterAIState.AttackType2);
                yield break;
            }
            else if (distance > monsterController.ReturnDistance)
            {
                monsterAI.UpdateState(Enums.MonsterAIState.Patrol);
                yield break;
            }
            monsterAI.Move(target.position);
            yield return CoroutineUtil.Instance.WaitForSeconds(CoroutineUtil.UPDATE_SECOND_0_01);
        }
    }
}
