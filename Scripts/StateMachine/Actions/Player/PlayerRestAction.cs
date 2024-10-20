using UnityEngine.InputSystem.XR;

public class PlayerRestAction : StateAction
{
    private PlayerController controller;

    private PlayerRestActionSO originSO => (PlayerRestActionSO)OriginSO;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
                
        controller = stateMachine.GetComponent<PlayerController>();
    }

    public override void OnStateEnter()
    {
        controller.IsRest = true;

        EventManager.Instance.TriggerEvent(Enums.EventType.UI_void_hideHUD);
        InGameManager.Instance.RestartGame();
        InGameManager.Instance.ShowMouse();
        PopupManager.Instance.Hide<UI_Popup_KeySign>();
        PopupManager.Instance.Show<UI_Popup_Rest>();
        InputManager.Instance.DisableInput();

        ResetPotion();
    }

    public override void OnStateExit()
    {
        ResetPotion();
        controller.ResetState();
        InGameManager.Instance.HideMouse();
        EventManager.Instance.TriggerEvent(Enums.EventType.UI_void_showHUD);
        PopupManager.Instance.Hide<UI_Popup_Rest>();
        PopupManager.Instance.Show<UI_Popup_KeySign>();
    }

    public override void OnUpdate()
    {
        
    }

    private void Save()
    {
        InGameManager.Instance.WritePlayTime();
        DataManager.Instance.CurrentSaveData.WritePostion(controller.transform.position);
        EventManager.Instance.TriggerEvent(Enums.EventType.Data_void_writeSaveData);
        DataManager.Instance.SaveGame();
    }

    private void ResetPotion()
    {
        int amount = controller.PlayerInventory.GetAmountByItemIndex(Constants.Item.CONSUMABLE_POTION);
        if (amount < originSO.PotionRefillAmount + controller.AdditionalPotionCount)
        {
            int count = originSO.PotionRefillAmount - amount + controller.AdditionalPotionCount;
            bool result = controller.PlayerInventory.Add(Constants.Item.CONSUMABLE_POTION, count);
            if (!result)
            {
                // TODO:: 인벤 꽉 처리
            }
        }

        Save();
    }
}