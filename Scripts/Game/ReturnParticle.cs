using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnParticle : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        PoolingManager.Instance.Push(this.name, this.gameObject);
    }
}
