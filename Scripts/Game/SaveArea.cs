using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SaveArea : MonoBehaviour
{
    [SerializeField]
    private LayerMask avaliableLayer;
    [SerializeField]
    private GameObject light;
    [SerializeField]
    private ParticleSystem particle;
    [SerializeField]
    private int titleIndex;

    private bool isInit = false;
    private bool isFire = false;
    private PlayerController controller;
    private UI_Popup_KeySign keySignPopup;

    private void OnEnable()
    {
        light.SetActive(false);
        EventManager.Instance.AddListener(Enums.EventType.Data_void_applySaveData, ApplyData);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener(Enums.EventType.Data_void_applySaveData, ApplyData);
    }


    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer(Constants.Game.STRING_PLAYER))
            return;

        if (controller == null)
        {
            controller = other.GetComponent<PlayerController>();
            keySignPopup = PopupManager.Instance.Get<UI_Popup_KeySign>();
        }

        SetInteraction();

        EventManager.Instance.AddListener(Enums.EventType.Player_void_interaction, InteractionEvent);        
        PopupManager.Instance.Show<UI_Popup_KeySign>();

        controller.IsInSaveArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer(Constants.Game.STRING_PLAYER))
            return;

        controller.IsInSaveArea = false;
        PopupManager.Instance.Hide<UI_Popup_KeySign>();
        EventManager.Instance.RemoveListener(Enums.EventType.Player_void_interaction, InteractionEvent);
    }

    private void SetInteraction()
    {
        int interactionStringIndex = isFire ? Constants.StringIndex.BONFIRE_REST : Constants.StringIndex.BONFIRE_ENABLE;
        string interactionKey = InputManager.Instance.GetInteractionKey();
        string interactionString = DataManager.Instance.GetString(interactionStringIndex);

        keySignPopup.SetKeySign(interactionKey, interactionString);
    }

    private void InteractionEvent()
    {
        if (!isFire)
        {
            isFire = true;
            EnableBonfire();
            SetInteraction();

            DataManager.Instance.CurrentSaveData.WriteBonfire(titleIndex);
            SoundManager.Instance.PlaySFX(Enums.SFX.BonfireEnable);
        }

        controller.IsRest = true;
    }

    private void EnableBonfire()
    {
        light.SetActive(true);
        particle.Play();
        SoundManager.Instance.PlaySFX3D(Enums.SFX.Bonfire, transform.position, true, 1, 7);
    }

    private void ApplyData()
    {
        SaveBonfireData data = DataManager.Instance.CurrentSaveData.TryGetBonfireData(titleIndex);

        if (data != null)
        {
            isFire = true;
            EnableBonfire();
        }

    }
}
