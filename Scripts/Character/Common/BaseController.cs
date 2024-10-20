using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField]
    private Transform lookAtTarget;

    public Transform LookAtTarget { get => lookAtTarget; }
    public bool IsPerformingAction { get; set; }
    public abstract bool IsDead { get; }

    public void Awake()
    {
        Init();
    }

    public virtual void Init()
    {

    }
}
