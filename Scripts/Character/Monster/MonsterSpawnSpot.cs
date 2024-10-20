using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnSpot : MonoBehaviour
{
    [SerializeField]
    private Enums.MonsterType monsterType;

    private GameObject spawnObj;

    [ContextMenu("spawn")]
    public void Spawn()
    {
        GameObject obj = PoolingManager.Instance.Pop(monsterType.ToString());
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);

        spawnObj = obj;
    }

    public void ResetSpawn()
    {
        if (spawnObj == null)
            return;

        PoolingManager.Instance.Push(monsterType.ToString(), spawnObj);
        spawnObj = null;
    }
}
