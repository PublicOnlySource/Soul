using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : IStateComponent
{
    private bool isCached = false;
    private bool cachedStatement = default;
    private StateConditionSO originSO;

    public StateConditionSO OriginSO { get => originSO; }

    public virtual void Init(StateMachine stateMachine, StateConditionSO originSO) 
    {
        this.originSO = originSO;
    }
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    protected abstract bool Statement();

    public bool GetStatement()
    {
        if (!isCached)
        {
            isCached = true;
            cachedStatement = Statement();
        }

        return cachedStatement;
    }

    public void ClearCache()
    {
        isCached = false;
    }
}
