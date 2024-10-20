using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_CrossFade_", menuName = "Soul/State Machines/Actions/Animator CrossFade Action")]
public class AnimatorCrossFadeActionSO : StateActionSO
{
    [SerializeField]
    private Enums.SpecificMoment whenToRun;
    [SerializeField]
    private string animationName;
    [SerializeField]
    private int layerIndex;
    [SerializeField]
    private float normalizedTransitionDuration = 0.2f;

    public Enums.SpecificMoment WhenToRun { get => whenToRun; }
    public string AnimationName { get => animationName; }
    public float NormalizedTransitionDuration { get => normalizedTransitionDuration; }
    public int LayerIndex { get => layerIndex; }

    protected override StateAction CreateAction()
    {
        return new AnimatorCrossFadeAction();
    }
}
