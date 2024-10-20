using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup_Upgrade : UI_Popup
{
    [SerializeField]
    private TMP_Text totalSoulText;
    [SerializeField]
    private TMP_Text currentSoulText;
    [SerializeField]
    private Button upgradeButton;
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private List<UI_UpgradeSlot> upgradeSlots;

    private int level;
    private int totalSoul;

    public override void Init()
    {
        AddTotalSoul(0);
        

        upgradeButton.onClick.AddListener(() => Upgrade());
        backButton.onClick.AddListener(() => Back());

        for (int i = 0; i < upgradeSlots.Count; i++)
        {
            upgradeSlots[i].Init((value) => AddTotalSoul(value));
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.AddListener(Enums.EventType.UI_void_completeUpgrade, CompleteUpgrade);
        currentSoulText.text = DataManager.Instance.CurrentSaveData.Soul.ToString();
    }

    private void OnDisable()
    {        
        EventManager.Instance.RemoveListener(Enums.EventType.UI_void_completeUpgrade, CompleteUpgrade);
    }

    private void AddTotalSoul(int value)
    {
        totalSoul += value;
        totalSoulText.text = totalSoul.ToString();
        totalSoulText.color = Color.white;

        if (totalSoul > DataManager.Instance.CurrentSaveData.Soul)
        {
            totalSoulText.color = Color.red;
        }
    }

    private void Upgrade()
    {
        UpgradePaymentData data = new UpgradePaymentData();
        data.totalSoul = totalSoul;
        
        for (int i = 0;i < upgradeSlots.Count;i++)
        {
            if (upgradeSlots[i].UpgradeType == Enums.UpgradeType.Health)
            {
                data.healthUpgradeLevel = upgradeSlots[i].Level;
                data.healthUpgradeValue = upgradeSlots[i].Value;
            }
            else if (upgradeSlots[i].UpgradeType == Enums.UpgradeType.Stamina)
            {
                data.staminaUpgradeLevel = upgradeSlots[i].Level;
                data.staminaUpgradeValue = upgradeSlots[i].Value;
            }
            else if (upgradeSlots[i].UpgradeType == Enums.UpgradeType.Damage)
            {
                data.damageUpgradeLevel = upgradeSlots[i].Level;
                data.damageUpgradeValue = upgradeSlots[i].Value;
            }
            else if (upgradeSlots[i].UpgradeType == Enums.UpgradeType.Potion)
            {
                data.potionUpgradeLevel = upgradeSlots[i].Level;
                data.potionUpgradeValue = upgradeSlots[i].Value;
            }
            else if (upgradeSlots[i].UpgradeType == Enums.UpgradeType.PotionCount)
            {
                data.potionCountUpgradeLevel = upgradeSlots[i].Level;
                data.potionCountUpgradeValue = upgradeSlots[i].Value; 
            }
        }

        EventManager.Instance.TriggerEvent(Enums.EventType.Player_UpgradePaymentData_upgrade, data);
    }

    private void CompleteUpgrade()
    {
        totalSoulText.text = Constants.Game.STRING_ZERO;
        totalSoul = 0;
        currentSoulText.text = DataManager.Instance.CurrentSaveData.Soul.ToString();

        for (int i = 0; i < upgradeSlots.Count;i++)
        {
            upgradeSlots[i].UpdateSlot();
        }
    }

    private void Back()
    {
        PopupManager.Instance.Show<UI_Popup_Rest>();
        PopupManager.Instance.Hide<UI_Popup_Upgrade>();
    }
}
