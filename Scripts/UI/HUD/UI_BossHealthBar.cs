using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BossHealthBar : UI_BaseHealthBar
{
    [Header("Boss Health Option")]
    [SerializeField]
    private TMP_Text bossNameText;

    private void OnEnable()
    {
        EventManager.Instance.AddListener<string>(Enums.EventType.UI_string_updateBossNameText, UpdateBossNameText);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener<string>(Enums.EventType.UI_string_updateBossNameText, UpdateBossNameText);
    }

    private void UpdateBossNameText(string bossName)
    {
        bossNameText.text = bossName;
    }
}
