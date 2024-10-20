using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private SaveData currentSaveData;
    private Dictionary<string, SaveData> datas = new Dictionary<string, SaveData>();
    private SaveFileDataWriter saveFileDataWriter = new SaveFileDataWriter();
    private Dictionary<int, ConsumableItemData> consumableItemDatas = new Dictionary<int, ConsumableItemData>();
    private Dictionary<int, EquipmentItemData> equimentItemDatas = new Dictionary<int, EquipmentItemData>();
    private Dictionary<int, StringData> stringDatas = new Dictionary<int, StringData>();
    private Dictionary<int, UpgradeData> upgradeDatas = new Dictionary<int, UpgradeData>();
    private Dictionary<int, MonsterData> monsterDatas = new Dictionary<int, MonsterData>();

    public SaveData CurrentSaveData { get => currentSaveData; }
    public Dictionary<string, SaveData> Datas { get => datas; }

    private void LoadCsvData<T>(string csvName, ref Dictionary<int, T> datas) where T : new()
    {
        TextAsset textAsset = AddressableManager.Instance.GetCsv(csvName);
        datas = CSVReader.ReadCSV<T>(textAsset);
    }

    public void Init()
    {
        saveFileDataWriter.Init();

        LoadCsvData<ConsumableItemData>(Constants.Addressable.CSV_CONSUMABLEITEMS, ref consumableItemDatas);
        LoadCsvData<EquipmentItemData>(Constants.Addressable.CSV_EQUIMENTITEMS, ref equimentItemDatas);
        LoadCsvData<StringData>(Constants.Addressable.CSV_STRING, ref stringDatas);
        LoadCsvData<UpgradeData>(Constants.Addressable.CSV_UPGRADE, ref upgradeDatas);
        LoadCsvData<MonsterData>(Constants.Addressable.CSV_MONSTER, ref monsterDatas);
    }

    public void SaveGame()
    {
        saveFileDataWriter.UpdateSaveFile(currentSaveData);
    }

    public void LoadAll()
    {
        datas = saveFileDataWriter.LoadSaveFileAll();
    }

    public void LoadGame(int index)
    {
        string fileName = saveFileDataWriter.GetFileName(index);

        if (!datas.TryGetValue(fileName, out SaveData data))
        {
            currentSaveData = new SaveData();            
        }
        else
        {
            currentSaveData = data;
        }
    }

    public string GetFileName(int index)
    {
        return saveFileDataWriter.GetFileName(index);
    }

    public BaseItemData GetItemData(int index)
    {
        if (consumableItemDatas.TryGetValue(index, out ConsumableItemData ci))
            return ci;

        if (equimentItemDatas.TryGetValue(index, out EquipmentItemData ei))
            return ei;

        return null;
    }

    public List<UpgradeLevelData> GetUpgradeLevelDatas(Enums.UpgradeType upgradeType)
    {
        List<UpgradeLevelData> datas = new List<UpgradeLevelData>();

        for (int i = 1; i <= upgradeDatas.Count; i++)
        {
            if (upgradeDatas[i].Type == upgradeType)
            {
                UpgradeLevelData data = new UpgradeLevelData();
                data.level = upgradeDatas[i].Level;
                data.requireSoul = upgradeDatas[i].RequireSoul;
                data.value = upgradeDatas[i].Value;

                datas.Add(data);
            }
        }

        return datas.OrderBy((x) => x.level).ToList();
    }

    public int GetUpgradeValueByLevel(Enums.UpgradeType upgradeType, int level)
    {
        for (int i = 1; i <= upgradeDatas.Count; i++)
        {
            if (upgradeDatas[i].Type == upgradeType && upgradeDatas[i].Level == level)
            {
                return upgradeDatas[i].Value;
            }
        }

        return 0;
    }

    public MonsterData GetMonsterData(int index)
    {
        if (!monsterDatas.TryGetValue(index, out MonsterData monster))
            return null;

        return monster;
    }

    public string GetString(int index)
    {
        if (!stringDatas.TryGetValue(index, out StringData result))
            return string.Empty;

        // TODO:: 추후 언어에 따라 반환하도록 
        return result.Ko;
    }
   
}
