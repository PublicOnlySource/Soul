using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateActionSO : ScriptableObject
{
    public StateAction GetAction(StateMachine stateMachine, Dictionary<ScriptableObject, object> caching)
    {
        if (caching.TryGetValue(this, out object cache))
            return (StateAction)cache;

        StateAction action = CreateAction();
        caching.Add(this, action);

        action.Init(stateMachine, this);

        return action;
    }

    protected abstract StateAction CreateAction();
}
