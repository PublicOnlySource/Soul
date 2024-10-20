using ccm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using UnityEngine;
using Debug = ccm.Debug;

[CreateAssetMenu(fileName = "NewTransitionTable", menuName = "Soul/State Machines/Transition Table")]
public class TransitionTableSO : ScriptableObject
{
    [Serializable]
    public struct TransitionItem
    {
        public StateSO FromState;
        public StateSO ToState;
        public ConditionUsage[] Conditions;
    }

    [Serializable]
    public struct ConditionUsage
    {
        public Result ExpectedResult;
        public StateConditionSO Condition;
        public Operator Operator;
    }

    public enum Result { True, False }
    public enum Operator { And, Or }

    [SerializeField]
    private List<TransitionItem> transitions;

    public List<TransitionItem> Transitions { get => transitions; private set => transitions = value; }

    public State GetInitialState(StateMachine stateMachine)
    {
        List<State> states = new List<State>();
        List<StateTransition> stateTransitions = new List<StateTransition>();
        Dictionary<ScriptableObject, object> caching = new Dictionary<ScriptableObject, object>();

        var fromStates = transitions.GroupBy(transition => transition.FromState);

        foreach (var fromState in fromStates)
        {
            State state = fromState.Key.GetState(stateMachine, caching);
            states.Add(state);

            stateTransitions.Clear();
            foreach (var transition in fromState)
            {
                if (transition.ToState == null)
                {
                    Debug.Log(stateMachine.transform.name + "의 ToState가 비었습니다.");
                }

                var toState = transition.ToState.GetState(stateMachine, caching);
                CreateArrayConditionForTransition(stateMachine, transition.Conditions, caching, out StateCondition[] conditions, out int[] resultGroup);
                stateTransitions.Add(new StateTransition(toState, conditions, resultGroup));
            }

            state.transitions = (stateTransitions.ToArray());
        }

        return states[0];
    }

    private void CreateArrayConditionForTransition(StateMachine stateMachine, ConditionUsage[] conditionUsages, Dictionary<ScriptableObject, object> caching, out StateCondition[] conditions, out int[] resultGroups)
    {
        List<int> resultGroupsList = new List<int>();
        int count = conditionUsages.Length;
        conditions = new StateCondition[count];

        for (int i = 0; i < count; i++)
        {
            conditions[i] = conditionUsages[i].Condition.GetCondition(stateMachine, conditionUsages[i].ExpectedResult == Result.True, caching);
        }

        for (int i = 0; i < count; i++)
        {
            int idx = resultGroupsList.Count;
            resultGroupsList.Add(1);

            while (i < count - 1 && conditionUsages[i].Operator == Operator.And)
            {
                i++;
                resultGroupsList[idx]++;
            }
        }

        resultGroups = resultGroupsList.ToArray();
    }
}
