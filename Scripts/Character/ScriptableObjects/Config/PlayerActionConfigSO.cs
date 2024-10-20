using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerActionConfig", menuName = "Soul/Config/PlayerAction Config")]
public class PlayerActionConfigSO : ScriptableObject
{
    [Header("Movement")]
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float sprintSpeed;

    [Header("Stamina Cost")]
    [SerializeField]
    private float sprintCost;
    [SerializeField]
    private float rollCost;

    public float WalkSpeed { get => walkSpeed; }
    public float SprintSpeed { get => sprintSpeed; }
    public float SprintCost { get => sprintCost; }
    public float RollCost { get => rollCost; }
}
