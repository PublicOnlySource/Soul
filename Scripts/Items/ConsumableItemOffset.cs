using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItemOffset : MonoBehaviour
{
    [SerializeField]
    private Vector3 offsetPos;
    [SerializeField]
    private Vector3 offsetScale;

    public Vector3 OffsetPos { get => offsetPos; }
    public Vector3 OffsetScale { get => offsetScale; }
}
