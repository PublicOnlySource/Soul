using UnityEngine;

[CreateAssetMenu(fileName = "PlayerRestAction", menuName = "Soul/State Machines/Actions/PlayerRestAction")]
public class PlayerRestActionSO : StateActionSO
{
    [SerializeField]
    private int potionRefillAmount = 3;

    public int PotionRefillAmount { get => potionRefillAmount; }

    protected override StateAction CreateAction()
    {
        return new PlayerRestAction();
    }
}