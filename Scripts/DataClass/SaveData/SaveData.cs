using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public SavePlayerInfoData playerInfoData;
    //public SaveSettingData settingData;
    public List<SaveInventoryData> inventoryDatas;
    public List<SaveBonfireData> bonfireDatas;
    public List<SaveBossData> bossDatas;

    public SaveData()
    {
        playerInfoData = new SavePlayerInfoData();
        //settingData = new SaveSettingData();
        inventoryDatas = new List<SaveInventoryData>();
        bonfireDatas = new List<SaveBonfireData>();
        bossDatas = new List<SaveBossData>();
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this, true);
    }

    #region Write Data

    public void WritePostion(Vector3 pos)
    {
        playerInfoData.position = pos;
    }

    public void WritePlayTime(float time)
    {
        playerInfoData.playTime = time;
    }

    public void WriteInventory(ItemSlotData[] datas)
    {
        inventoryDatas = new List<SaveInventoryData>();
        SaveInventoryData saveData;

        for (int i = 0; i < datas.Length; i++)
        {
            if (datas[i] == null)
                continue;

            saveData = new SaveInventoryData();
            saveData.slotIndex = datas[i].SlotIndex;
            saveData.amount = datas[i].Amount;
            saveData.itemIndex = datas[i].ItemIndex;
            saveData.isWorn = datas[i].IsWorn;
            saveData.isRegisterQuickSlot = datas[i].IsRegisterQuickSlot;

            inventoryDatas.Add(saveData);
        }
    }

    public void WriteSoul(int soul)
    {
        playerInfoData.soul = soul;
        Debug.Log("soul save" + soul);
    }

    public void WriteSoulDropInfo(Vector3 pos, int soulAmount)
    {
        playerInfoData.soulDropPosition = pos;
        playerInfoData.soulDropAmount = soulAmount;
    }

    public void WriteUpgradeLevel(UpgradePaymentData data)
    {
        playerInfoData.healthUpgradeLevel = data.healthUpgradeLevel;
        playerInfoData.staminaUpgradeLevel = data.staminaUpgradeLevel;
        playerInfoData.damageUpgradeLevel = data.damageUpgradeLevel;
        playerInfoData.potionUpgradeLevel = data.potionUpgradeLevel;
        playerInfoData.potionCountUpgradeLevel = data.potionCountUpgradeLevel;


        playerInfoData.additionalMaxHealth = data.healthUpgradeValue;
        playerInfoData.additionalMaxStamina = data.staminaUpgradeValue;
        playerInfoData.additionalDamage = data.damageUpgradeValue;       
    }

    public void WriteBonfire(int titleIndex)
    {
        SaveBonfireData data = new SaveBonfireData();
        data.titleIndex = titleIndex;
        data.isLastSave = true;

        for (int i = 0; i < bonfireDatas.Count; i++)
        {
            bonfireDatas[i].isLastSave = false;
        }

        bonfireDatas.Add(data);
    }

    public void WriteBossClear(int index)
    {
        SaveBossData data = new SaveBossData();
        data.clearBossIndex = index;

        bossDatas.Add(data);
    }

    #endregion

    #region Get Data

    public Vector3 PlayerPosition { get => playerInfoData.position; }
    public float PlayTime { get => playerInfoData.playTime; }
    public int HealthUpgradeLevel { get => playerInfoData.healthUpgradeLevel; }
    public int StaminaUpgradeLevel { get => playerInfoData.staminaUpgradeLevel; }
    public int DamageUpgradeLevel { get => playerInfoData.damageUpgradeLevel; }
    public int PotionUpgradeLevel { get => playerInfoData.potionUpgradeLevel; }
    public int PotionCountUpgradeLevel { get => playerInfoData.potionCountUpgradeLevel; }
    public List<SaveInventoryData> Inventory { get => inventoryDatas; }
    public int Soul { get => playerInfoData.soul; }
    public Vector3 SoulDropPosition { get => playerInfoData.soulDropPosition; }
    public int SoulDropAmount { get => playerInfoData.soulDropAmount; }
    
    public SaveBonfireData TryGetBonfireData(int titleIndex)
    {
        for (int i = 0; i < bonfireDatas.Count; i++)
        {
            if (bonfireDatas[i].titleIndex == titleIndex)
                return bonfireDatas[i];
        }

        return null;
    }

    public int GetLastBonfireTitleIndex()
    {
        for (int i = 0; i < bonfireDatas.Count; i++)
        {
            if (bonfireDatas[i].isLastSave)
                return bonfireDatas[i].titleIndex;
        }

        return -1;
    }

    public bool IsClearBoss(int index)
    {
        for (int i = 0; i < bossDatas.Count; i++)
        {
            if (bossDatas[i].clearBossIndex == index)
                return true;
        }

        return false;
    }
    
    #endregion
}
