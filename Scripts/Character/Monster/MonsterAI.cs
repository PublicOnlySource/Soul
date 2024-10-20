using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Debug = ccm.Debug;

public class MonsterAI : MonoBehaviour
{
    [SerializeField]
    private Detector detector;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    [ReadOnly]
    private Enums.MonsterAIState state;

    [Header("Debug")]
    [SerializeField]
    private bool printAITransition;

    private Coroutine rotateStateCoroutine;

    private readonly Vector2 DEFAULT_STATE_CHANGE_SECOND_RANGE = new Vector2(3, 11);
    private readonly float RANDOM_POSITION_UPDATE_SECOND = 5;
    private readonly float RANDOM_POSITION_CIRCLE_RANGE = 7;

    public Enums.MonsterAIState State { get => state; set => state = value; }
    public float AgentRemainingDistance
    {
        get
        {
            if (!agent.isActiveAndEnabled)
                return 0;
            return agent.remainingDistance;
        }
    }
    public Detector Detector { get => detector; }

    private void OnEnable()
    {
        agent.Warp(transform.position);
        rotateStateCoroutine = StartCoroutine(RotateState());
    }

    private void OnDisable()
    {
        ResetAICoroutine();
    }

    private IEnumerator RotateState()
    {
        int from = (int)Enums.MonsterAIState.Idle;
        int to = (int)Enums.MonsterAIState.Patrol;
        Enums.MonsterAIState randomState;

        DisableAgent();

        while (true)
        {
            randomState = (Enums.MonsterAIState)Random.Range(from, to + 1);

            UpdateState(randomState);
            yield return CoroutineUtil.Instance.WaitForSeconds(Random.Range(DEFAULT_STATE_CHANGE_SECOND_RANGE.x, DEFAULT_STATE_CHANGE_SECOND_RANGE.y));
            yield return new WaitUntil(() => state == Enums.MonsterAIState.Idle || state == Enums.MonsterAIState.Patrol);
        }
    }

    public void UpdateState(Enums.MonsterAIState state)
    {
        if (this.state == state)
            return;

        if (printAITransition)
        {
            Debug.Log($"[AI] {gameObject.name} // {this.state} -> {state}");
        }

        this.state = state;
    }

    public bool GetRandomPosition(out Vector3 result)
    {
        Vector3 pos = transform.position + Random.insideUnitSphere * RANDOM_POSITION_CIRCLE_RANGE;

        if (NavMesh.SamplePosition(pos, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            result = pos;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    public void EnableAgent()
    {
        if (agent.enabled)
            return;

        agent.enabled = true;
        agent.Warp(transform.position);
    }

    public void DisableAgent()
    {
        if (!agent.enabled)
            return;

        agent.enabled = false;
    }

    public void ResetAICoroutine()
    {
        if (rotateStateCoroutine != null)
        {
            StopCoroutine(rotateStateCoroutine);
            rotateStateCoroutine = null;
        }
    }

    public void Move(Vector3 pos)
    {
        EnableAgent();
        agent.SetDestination(pos);
    }

    public void MoveDetectObject()
    {
        if (!detector.IsDetect)
            return;

        Move(detector.DetectObject.transform.position);
    }
}
