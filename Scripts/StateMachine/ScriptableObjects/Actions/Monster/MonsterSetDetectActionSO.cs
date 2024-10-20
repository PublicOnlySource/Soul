using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSetDetectAction", menuName = "Soul/State Machines/Actions/MonsterSetDetectAction")]
public class MonsterSetDetectActionSO : StateActionSO
{
    [SerializeField]
    private bool enable;
    [SerializeField]
    private bool useClear;
    [SerializeField]
    private Enums.SpecificMoment whenToRun;

    public bool Enable { get => enable; }
    public Enums.SpecificMoment WhenToRun { get => whenToRun; }
    public bool UseClear { get => useClear; }

    protected override StateAction CreateAction()
    {
        return new MonsterSetDetecetAction();
    }
}