using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoul : IUseEventNonMono
{
    private int soul;

    public int Soul { get => soul; }

    private void SaveSoul()
    {
        SaveData data = DataManager.Instance.CurrentSaveData;

        data.WriteSoul(soul);
        DataManager.Instance.SaveGame();
    }

    private void AfterModifySoul()
    {
        EventManager.Instance.TriggerEvent<int>(Enums.EventType.UI_int_updateSoulText, soul);
        SaveSoul();
    }

    public void AddSoul(int value)
    {
        if (value < 0)
            return;

        soul += value;
        AfterModifySoul();
    }

    public void SetSoul(int value)
    {
        if (value < 0)
            return;

        soul = value;
        AfterModifySoul();
    }

    public bool UseSoul(int value)
    {
        if (soul - value < 0)
            return false;

        soul -= value;
        AfterModifySoul();
        return true;
    }

    public void ApplyData()
    {
        SaveData data = DataManager.Instance.CurrentSaveData;
        AddSoul(data.Soul);
    }

    public void AddListeners()
    {
        EventManager.Instance.AddListener<int>(Enums.EventType.Player_int_add_soul, AddSoul);
        EventManager.Instance.AddListener(Enums.EventType.Data_void_applySaveData, ApplyData);
    }

    public void RemoveListeners()
    {
        EventManager.Instance.RemoveListener<int>(Enums.EventType.Player_int_add_soul, AddSoul);
        EventManager.Instance.RemoveListener(Enums.EventType.Data_void_applySaveData, ApplyData);
    }
}
