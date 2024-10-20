using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField]
    private TransitionTableSO transitionTableSO;

    [Header("DEBUG")]
    [SerializeField]
    private bool printTransition = false;
    [SerializeField]
    [ReadOnly]
    private string currentStateString;

    private State currentState;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        currentState.OnUpdate();

        if (currentState.CanTransition(out State state))
        {
            Transition(state);
        }
    }

    private void Init()
    {
        currentState = transitionTableSO.GetInitialState(this);
        currentState.OnStateEnter();
    }

    private void Transition(State state)
    {
        currentState.OnStateExit();

#if UNITY_EDITOR
        if (printTransition)
        {
            Debug.Log($"[State] {currentState.originSO.name} -> {state.originSO.name}");
        }

        currentStateString = state.originSO.name;
#endif

        currentState = state;
        currentState.OnStateEnter();
    }

}
