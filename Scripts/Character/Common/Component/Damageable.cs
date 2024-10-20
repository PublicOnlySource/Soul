using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField]
    private HealthConfigSO healthConfigSO;

    [Header("Health Bar Setting(option)")]
    [SerializeField]
    private Transform healthBarRoot;
    [SerializeField]
    private GameObject healthBarPrefab;
    [SerializeField]
    private Vector3 healthBarPosition;
    [SerializeField]
    private bool lookAtPlayer;
    [SerializeField]
    private bool isActive;

    private HealthData healthData;

    public bool IsHit { get; set; }
    public bool IsDead { get => healthData.IsDead; }
    public HealthData HealthData { get => healthData; }

    private void OnEnable()
    {
        if (healthData == null)
        {
            healthData = new HealthData(healthConfigSO);
        }

        ResetHealth();
    }

    private void Start()
    {
        CreateHealthBar();
    }

    private void CreateHealthBar()
    {
        if (healthBarPrefab == null)
            return;

        UI_BaseHealthBar bar = Instantiate(healthBarPrefab, healthBarRoot).GetComponent<UI_BaseHealthBar>();
        bar.Init();
        if (lookAtPlayer)
        {
            bar.EnableLookAt();
        }
        bar.SetHealthDataEvent(healthData);
        bar.SetPosition(healthBarPosition);
        bar.transform.SetAsFirstSibling();
        healthData.Init();

        bar.gameObject.SetActive(isActive);
    }

    public void TakeDamage(float damage)
    {
        healthData.TakeDamage(damage);
        IsHit = true;
    }

    public void ResetHealth()
    {
        healthData.Reset();
        SetActiveHealthBar(true);
    }

    public void RecoveryHealth(float value)
    {
        healthData.AddHealth(value);
    }

    public void SetActiveHealthBar(bool value)
    {
        if (healthData == null)
            return;

        healthData.SetActiveHealthBar(value);
    }
}
