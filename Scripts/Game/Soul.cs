using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    private UI_Popup_KeySign keySignPopup;
    private bool isInit = false;
    private int soulAmount;

    private void OnEnable()
    {
        EventManager.Instance.AddListener(Enums.EventType.Player_void_death, ResetWithPool);       
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener(Enums.EventType.Player_void_death, ResetWithPool);        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer(Constants.Game.STRING_PLAYER))
            return;

        if (other.GetComponent<Damageable>().IsDead)
            return;

        Init();
        keySignPopup.SetKeySign("E", "회수한다.");
        EventManager.Instance.AddListener(Enums.EventType.Player_void_interaction, InteractionEvent);
        PopupManager.Instance.Show<UI_Popup_KeySign>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer(Constants.Game.STRING_PLAYER))
            return;

        Reset();
    }

    private void Init()
    {
        if (isInit)
            return;

        keySignPopup = PopupManager.Instance.Get<UI_Popup_KeySign>();      
       
        isInit = true;
    }

    private void InteractionEvent()
    {
        ResetWithPool();
        EventManager.Instance.TriggerEvent<int>(Enums.EventType.Player_int_add_soul, soulAmount);
    }

    private void Reset()
    {
        EventManager.Instance.RemoveListener(Enums.EventType.Player_void_interaction, InteractionEvent);
        PopupManager.Instance.Hide<UI_Popup_KeySign>();
    }

    private void ResetWithPool()
    {
        Reset();
        SaveSoulSpawnPos(true);
        PoolingManager.Instance.Push(Constants.Prefab.NAME_GAME_OBJECTS_SOUL, this.gameObject);
    }

    public void SaveSoulSpawnPos(bool isReset = false)
    {
        SaveData data = DataManager.Instance.CurrentSaveData;

        if (!isReset && data.SoulDropPosition != Vector3.zero)
            return;

        data.WriteSoulDropInfo(isReset ? Vector3.zero : transform.position, soulAmount);
        data.WriteSoul(0);

        DataManager.Instance.SaveGame();
    }

    public void SetSoulAmount(int value)
    {
        soulAmount = value;
    }
}
