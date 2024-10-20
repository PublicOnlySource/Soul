using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HUDController : MonoBehaviour
{
    [SerializeField]
    private Image lockOnImage;
    [SerializeField]
    private UI_QuickSlot quickSlot;

    public UI_QuickSlot QuickSlot { get => quickSlot; }

    private void OnEnable()
    {
        EventManager.Instance.AddListener<bool>(Enums.EventType.UI_bool_setActiveLockOnImage, SetActiveLockOnImage);
        EventManager.Instance.AddListener<Vector3>(Enums.EventType.UI_vector3_setPositionLockOnImage, SetPositionLockOnImage);
        EventManager.Instance.AddListener(Enums.EventType.UI_void_hideHUD, Hide);
        
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener<bool>(Enums.EventType.UI_bool_setActiveLockOnImage, SetActiveLockOnImage);
        EventManager.Instance.RemoveListener<Vector3>(Enums.EventType.UI_vector3_setPositionLockOnImage, SetPositionLockOnImage);
        EventManager.Instance.RemoveListener(Enums.EventType.UI_void_hideHUD, Hide);
    }

    private void SetActiveLockOnImage(bool value)
    {
        lockOnImage.gameObject.SetActive(value);
    }

    private void SetPositionLockOnImage(Vector3 value)
    {
        lockOnImage.transform.position = value;
    }

    private void Hide()
    {
        EventManager.Instance.AddListener(Enums.EventType.UI_void_showHUD, Show);
        gameObject.SetActive(false);
    }

    private void Show()
    {
        EventManager.Instance.RemoveListener(Enums.EventType.UI_void_showHUD, Show);
        gameObject.SetActive(true);
    }
}
