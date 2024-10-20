using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuickSlot : MonoBehaviour
{
    [SerializeField]
    private Image iconImage;
    [SerializeField]
    private TMP_Text amountText;

    private ItemSlotData slotData;
    private Coroutine coroutine;

    private void Awake()
    {
        Clear();
    }

    private void OnEnable()
    {
        EventManager.Instance.AddListener<ItemSlotData>(Enums.EventType.UI_ItemSlotData_quickSlot_Register, Register);
        EventManager.Instance.AddListener(Enums.EventType.UI_void_quickSlotClear, Clear);
        EventManager.Instance.AddListener(Enums.EventType.UI_void_quickSLotUpdate, UpdateQuickSlot);
        coroutine = StartCoroutine(Process());
    }

    private void OnDisable()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveListener<ItemSlotData>(Enums.EventType.UI_ItemSlotData_quickSlot_Register, Register);
        EventManager.Instance.RemoveListener(Enums.EventType.UI_void_quickSlotClear, Clear);
        EventManager.Instance.RemoveListener(Enums.EventType.UI_void_quickSLotUpdate, UpdateQuickSlot);
    }

    private IEnumerator Process()
    {
        while (true)
        {
            yield return new WaitUntil(() => slotData != null);
            yield return new WaitUntil(() => InputManager.Instance.QuickSlotInput);

            EventManager.Instance.TriggerEvent<int>(Enums.EventType.Inventory_int_use, slotData.SlotIndex);

            yield return new WaitUntil(() => !InputManager.Instance.QuickSlotInput);
        }
    }

    private void Register(ItemSlotData data)
    {
        if (slotData != null)
        {
            slotData.IsRegisterQuickSlot = false;            
        }

        data.IsRegisterQuickSlot = true;
        slotData = data;        
        UpdateUI();

        DataManager.Instance.SaveGame();
    }

    private void UpdateUI()
    {
        if (slotData.Amount <= 0)
        {
            Clear();
            return;
        }

        iconImage.sprite = AddressableManager.Instance.GetSprite(Constants.Addressable.ATLAS_CONSUMABLE, slotData.ItemIndexString);
        amountText.text = slotData.AmountString;
        ShowItemInfo();
    }

    private void ShowItemInfo()
    {
        iconImage.gameObject.SetActive(true);
        amountText.gameObject.SetActive(true);
    }

    private void HideItemInfo()
    {
        iconImage.gameObject.SetActive(false);
        amountText.gameObject.SetActive(false);
    }

    private void Clear()
    {
        slotData = null;
        iconImage.sprite = null;
        amountText.text = string.Empty;
        HideItemInfo();
    }

    public void UpdateQuickSlot()
    {
        if (slotData == null)
            return;

        UpdateUI();
    }

    public int GetItemSlotIndex()
    {
        if (slotData == null)
            return -1;

        return slotData.SlotIndex;
    }
}
