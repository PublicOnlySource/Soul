using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private List<MonsterSpawnSpot> spawnSpots;

    private void OnEnable()
    {
        EventManager.Instance.AddListener(Enums.EventType.Monster_void_spawn, Spawn);
        EventManager.Instance.AddListener(Enums.EventType.Monster_void_allDestory, ResetSpawn);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener(Enums.EventType.Monster_void_spawn, Spawn);
        EventManager.Instance.RemoveListener(Enums.EventType.Monster_void_allDestory, ResetSpawn);
    }

    private void Spawn()
    {
        for (int i = 0; i < spawnSpots.Count; i++)
        {
            if (!spawnSpots[i].gameObject.activeSelf)
                continue;

            spawnSpots[i].Spawn();
        }
    }

    private void ResetSpawn()
    {
        for (int i = 0; i < spawnSpots.Count; i++)
        {
            if (!spawnSpots[i].gameObject.activeSelf)
                continue;

            spawnSpots[i].ResetSpawn();
        }
    }
}
