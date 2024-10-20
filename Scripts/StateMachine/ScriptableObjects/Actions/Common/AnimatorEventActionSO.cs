using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEventActionSO : StateActionSO
{
    [SerializeField]
    private string stateName;
    [SerializeField]
    private Enums.InequalitySign inequalitySign;
    [SerializeField]
    private float targetNormalizedTime;
    [SerializeField]
    private int layerIndex = 0;

    public string StateName { get => stateName; }
    public Enums.InequalitySign InequalitySign { get => inequalitySign; }
    public float TargetNormalizedTime { get => targetNormalizedTime; }
    public int LayerIndex { get => layerIndex; }

    protected override StateAction CreateAction()
    {
       return new AnimatorEventAction();
    }
}
