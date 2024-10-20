using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthConfig", menuName = "Soul/Config/Health Config")]
public class HealthConfigSO : ScriptableObject
{
    [SerializeField]
    private int initialHealth;


    public int InitialHealth => initialHealth;

}
