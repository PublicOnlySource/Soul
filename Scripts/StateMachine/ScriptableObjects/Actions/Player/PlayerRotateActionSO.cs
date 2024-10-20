using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerRotateAction", menuName = "Soul/State Machines/Actions/PlayerRotate")]
public class PlayerRotateActionSO : StateActionSO
{
    [SerializeField]
    private float rotateSpeed;

    public float RotateSpeed { get => rotateSpeed; }

    protected override StateAction CreateAction()
    {
       return new PlayerRotateAction();
    }
}
