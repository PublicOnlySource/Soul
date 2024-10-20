using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumableItemSO : BaseItemSO
{
    [SerializeField]
    private Vector3 motionOffset = Vector3.zero;

    public Vector3 MotionOffset { get => motionOffset;  }
}
