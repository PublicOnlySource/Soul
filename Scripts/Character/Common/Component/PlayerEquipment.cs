using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerEquitment : IUseEventNonMono
{
    private Transform weaponRoot;
    private Attacker attacker;
    private GameObject equipment;
    private EquipmentItemData itemData;
    private Coroutine coroutine;

    public EquipmentItemData ItemData { get => itemData; }

    public PlayerEquitment(Transform weaponRoot, Attacker attacker)
    {
        this.weaponRoot = weaponRoot;
        this.attacker = attacker;
    }

    private void UnloadWeapon()
    {
        if (equipment == null)
            return;

        PoolingManager.Instance.Push(itemData.Index.ToString(), equipment);
        equipment = null;
    }

    private IEnumerator LoadWeapon()
    {
        equipment = PoolingManager.Instance.Pop(itemData.Index.ToString(), true);     
        equipment.transform.SetParent(weaponRoot);
        equipment.transform.localPosition = Vector3.zero;
        equipment.transform.localRotation = Quaternion.identity;

        yield return new WaitUntil(() => equipment != null);
        attacker.SetAttackComponent(equipment.transform, itemData.Damage);
    }

    public void Load(EquipmentItemData data)
    {
        UnloadWeapon();
        itemData = data;

        if (coroutine != null)
        {
            CoroutineUtil.Instance.StopCoroutine(coroutine);
            coroutine = null;
        }

        coroutine = CoroutineUtil.Instance.StartCoroutine(LoadWeapon());
    }
       
    public void ShowWeapon()
    {
        equipment.SetActive(true);
    }

    public void HideWeapon()
    {
        equipment.SetActive(false);
    }

    public void AddListeners()
    {
        EventManager.Instance.AddListener<EquipmentItemData>(Enums.EventType.Player_EquipmentItemData_loadEquipment, Load);
    }

    public void RemoveListeners()
    {
        EventManager.Instance.RemoveListener<EquipmentItemData>(Enums.EventType.Player_EquipmentItemData_loadEquipment, Load);
    }
}
