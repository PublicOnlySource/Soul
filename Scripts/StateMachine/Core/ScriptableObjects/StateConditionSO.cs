using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateConditionSO : ScriptableObject
{
    public StateCondition GetCondition(StateMachine stateMachine, bool exceptedResult, Dictionary<ScriptableObject, object> caching)
    {
        if (caching.TryGetValue(this, out object cache))
            return new StateCondition(stateMachine, (Condition)cache, exceptedResult);

        Condition condition = CreateCondition();
        condition.Init(stateMachine, this);
        caching.Add(this, condition);

        return new StateCondition(stateMachine, condition, exceptedResult);
    }

    protected abstract Condition CreateCondition();
}
