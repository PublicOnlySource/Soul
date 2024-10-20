using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class EnterBossArea : MonoBehaviour
{
    [SerializeField]
    private Enums.MonsterType type;
    [SerializeField]
    private int bossNameIndex;
    [SerializeField]
    private GameObject block;
    [SerializeField]
    private BoxCollider boxCollider;

    private void Awake()
    {
        block.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.Instance.AddListener(Enums.EventType.Monster_void_spawn, Close);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer(Constants.Game.STRING_PLAYER))
            return;

        if (DataManager.Instance.CurrentSaveData.IsClearBoss((int)type))
            return;

        string bossName = DataManager.Instance.GetString(bossNameIndex);

        block.SetActive(true);
        boxCollider.enabled = false;
        EventManager.Instance.TriggerEvent(Enums.EventType.Boss_void_Enter);
        EventManager.Instance.TriggerEvent<string>(Enums.EventType.UI_string_updateBossNameText, bossName);
    }

    private void Close()
    {
        block.SetActive(false);
        boxCollider.enabled = true;
    }
}
