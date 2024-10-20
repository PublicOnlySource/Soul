using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpgradeSlot : MonoBehaviour
{
    [SerializeField]
    private Enums.UpgradeType upgradeType;
    [SerializeField]
    private TMP_Text titleText;
    [SerializeField]
    private TMP_Text originStatText;
    [SerializeField]
    private TMP_Text upgradeStatText;
    [SerializeField]
    private Button downButton;
    [SerializeField]
    private Button upButton;

    private int level;
    private int originLevel;
    private List<UpgradeLevelData> levelDatas;
    private Action<int> addTotalSoulEvent;

    public Enums.UpgradeType UpgradeType { get => upgradeType; }
    public int Level { get => level; }
    public int Value { get => level == 0 ? 0 : levelDatas[level - 1].value; }

    private void InitLevel()
    {
        switch (upgradeType)
        {
            case Enums.UpgradeType.Health:
                level = DataManager.Instance.CurrentSaveData.HealthUpgradeLevel;                
                break;
            case Enums.UpgradeType.Stamina:
                level = DataManager.Instance.CurrentSaveData.StaminaUpgradeLevel;
                break;
            case Enums.UpgradeType.Damage:
                level = DataManager.Instance.CurrentSaveData.DamageUpgradeLevel;
                break;
            case Enums.UpgradeType.Potion:
                level = DataManager.Instance.CurrentSaveData.PotionUpgradeLevel;
                break;
            case Enums.UpgradeType.PotionCount:
                level = DataManager.Instance.CurrentSaveData.PotionCountUpgradeLevel;
                break;
        }

        originLevel = level;
    }

    private void InitButton()
    {
        if (originLevel == levelDatas.Count - 1)
        {
            downButton.gameObject.SetActive(false);
            upButton.gameObject.SetActive(false); 
            return;
        }

        downButton.onClick.AddListener(() => LevelDown());
        upButton.onClick.AddListener(() => LevelUp());
    }

    private void UpdateOriginStatText()
    {
        originStatText.text = originLevel == 0 ? Constants.Game.STRING_ZERO : levelDatas[originLevel - 1].value.ToString();
    }

    private void UpdateUpgradeStatText()
    {
        string text = Constants.Game.STRING_ZERO;

        if (originLevel == levelDatas.Count)
        {
            text = DataManager.Instance.GetString(Constants.StringIndex.UPGRADE_MAX_LEVEL);
        }
        else if (level > 0)
        {
            text = levelDatas[level - 1].value.ToString();
        }

        upgradeStatText.text = text;
    }

    private void LevelUp()
    {
        if (level == levelDatas.Count)
            return;

        level++;
        UpdateUpgradeStatText();
        addTotalSoulEvent?.Invoke(levelDatas[level - 1].requireSoul);
    }

    private void LevelDown()
    {
        if (level == originLevel)
            return;

        level--;
        UpdateUpgradeStatText();
        addTotalSoulEvent?.Invoke(-levelDatas[level].requireSoul);
    }

    public void Init(Action<int> addTotalSoulEvent)
    {
        this.addTotalSoulEvent = addTotalSoulEvent;
        levelDatas = DataManager.Instance.GetUpgradeLevelDatas(upgradeType);

        InitLevel();
        InitButton();

        UpdateOriginStatText();
        UpdateUpgradeStatText();
    }


    public void UpdateSlot()
    {
        InitLevel();

        UpdateOriginStatText();
        UpdateUpgradeStatText();
    }
}
