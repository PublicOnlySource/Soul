using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventoryClickMenu : MonoBehaviour
{
    [SerializeField]
    private RectTransform rt;
    [SerializeField]
    private GameObject rootLeftMenu;
    [SerializeField]
    private GameObject rootRightMenu;

    [Header("Buttons")]
    [SerializeField]
    private Button useButton;
    [SerializeReference]
    private Button removeButton;
    [SerializeField]
    private Button addQuickSlotButtons;

    private int slotIndex;

    private void OnDisable()
    {
        Hide();
    }

    public void AddRightButtonEvent()
    {
        useButton.onClick.AddListener(() =>
        {
            EventManager.Instance.TriggerEvent<int>(Enums.EventType.Inventory_int_use, slotIndex);
        });

        removeButton.onClick.AddListener(() =>
        {
            EventManager.Instance.TriggerEvent<int>(Enums.EventType.Inventory_int_remove, slotIndex);
        });
    }

    public void AddLeftButtonEvent(Action<int> buttonEvent)
    {
        addQuickSlotButtons.onClick.AddListener(() =>
        {
            buttonEvent?.Invoke(slotIndex);
        });
    }

    public void ShowMenu(bool isLeft, int index, RectTransform rt)
    {
        this.slotIndex = index;

        Show();
        if (isLeft)
        {
            rootLeftMenu.SetActive(true);
        }
        else
        {
            rootRightMenu.SetActive(true);
        }

        UpdatePos(rt);
    }

    private void UpdatePos(RectTransform slotRT)
    {
        rt.position = slotRT.position;
    }

    public void Show()
    {
        HideDetailMenu();
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        HideDetailMenu();
        this.gameObject.SetActive(false);
    }

    public bool IsShow()
    {
        return this.gameObject.activeSelf;
    }

    private void HideDetailMenu()
    {
        rootLeftMenu.SetActive(false);
        rootRightMenu.SetActive(false);
    }
}
