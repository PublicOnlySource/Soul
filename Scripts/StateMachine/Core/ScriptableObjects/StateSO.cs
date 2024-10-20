using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "Soul/State Machines/State")]
public class StateSO : ScriptableObject
{
    [SerializeField]
    private StateActionSO[] actions;

    public State GetState(StateMachine stateMachine, Dictionary<ScriptableObject, object> caching)
    {
        if (caching.TryGetValue(this, out object cache))
            return (State)cache;

        State state = new State();
        caching.Add(this, state);

        state.originSO = this;
        state.stateMachine = stateMachine;
        state.transitions = new StateTransition[0];
        state.actions = GetActions(stateMachine, caching);

        return state;
    }

    private StateAction[] GetActions(StateMachine stateMachine, Dictionary<ScriptableObject, object> caching)
    {
        int count = actions.Length;
        StateAction[] result = new StateAction[count];

        for (int i = 0; i < count; i++)
        {
            result[i] = actions[i].GetAction(stateMachine, caching);
        }

        return result;
    }
}
