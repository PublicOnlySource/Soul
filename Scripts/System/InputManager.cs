using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager> 
{
    [Header("Player Movement Input")]
    private Vector2 playerMovementInput;
    private float horizontalInput;
    private float verticalInput;
    private float moveAmount;

    [Header("Player Action Input")]
    private bool rollInput;
    private float runInput;
    private bool isAttack;
    private bool isBlock;
    private bool quickSlotInput;
    private bool isLockOn;
    private bool isMenu;

    [Header("Camera Movement Input")]
    private Vector2 cameraMovementInput;
    private float cameraHorizontalInput;
    private float cameraVerticalInput;

    private PlayerInputs playerInput;
    private PlayerInputs alwaysPlayerInput;
    private InputActionRebindingExtensions.RebindingOperation rebinding;

    public float MoveAmount { get => moveAmount; set => moveAmount = value; }
    public float HorizontalInput { get => horizontalInput; set => horizontalInput = value; }
    public float VerticalInput { 
        get => verticalInput; 
        private set
        {
            if (value < 0) verticalInput = -1;
            else if (value > 0) verticalInput = 1;
            else verticalInput = 0;
        }
    }

    public float CameraHorizontalInput { get => cameraHorizontalInput; }
    public float CameraVerticalInput { get => cameraVerticalInput; }
    public bool RollInput { get => rollInput; }
    public float RunInput { get => runInput; }
    public bool QuickSlotInput {  get => quickSlotInput; }
    public bool IsAttack { get => isAttack; }
    public bool IsBlock { get => isBlock; }
    public bool IsLockOn { get => isLockOn; }
    public bool IsMenu { get => isMenu; }

    private void OnEnable() {
        if (playerInput == null)
        {
            playerInput = new PlayerInputs();
            alwaysPlayerInput = new PlayerInputs();

            playerInput.PlayerMovement.Movement.performed += i => playerMovementInput = i.ReadValue<Vector2>();
            playerInput.PlayerCamera.Movement.performed += i => cameraMovementInput = i.ReadValue<Vector2>();

            playerInput.PlayerActions.Roll.performed += i => rollInput = true;
            playerInput.PlayerActions.Roll.canceled += i => rollInput = false;

            playerInput.PlayerActions.Run.performed += i => runInput = 1;
            playerInput.PlayerActions.Run.canceled += i => runInput = 0;

            playerInput.PlayerActions.Attack.performed += i => isAttack = true;
            playerInput.PlayerActions.Attack.canceled += i => isAttack = false;

            playerInput.PlayerActions.QuickSlot.performed += i => quickSlotInput = true;
            playerInput.PlayerActions.QuickSlot.canceled += i => quickSlotInput = false;

            playerInput.PlayerActions.LockOn.performed += i => isLockOn = true;
            playerInput.PlayerActions.LockOn.canceled += i => isLockOn = false;

            playerInput.PlayerActions.Interaction.performed += i => 
            {
                EventManager.Instance.TriggerEvent(Enums.EventType.Player_void_interaction);
            };

            playerInput.PlayerActions.Block.performed += i => isBlock = true;
            playerInput.PlayerActions.Block.canceled += i => isBlock = false;

            alwaysPlayerInput.PlayerActions.Menu.performed += i => isMenu = true;
            alwaysPlayerInput.PlayerActions.Menu.canceled += i => isMenu = false;
        }

        playerInput.Enable();
        alwaysPlayerInput.Enable();
    }

    private void Update()
    {
        UpdatePlayerMovementInput();
        UpdateCameraMovementInput();
    }

    private void UpdatePlayerMovementInput()
    {
        VerticalInput = playerMovementInput.y;
        HorizontalInput = playerMovementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(VerticalInput) + Mathf.Abs(HorizontalInput));

        if (moveAmount > 0) 
        {
            moveAmount += runInput;
        }
    }

    private void UpdateCameraMovementInput()
    {
        cameraVerticalInput = cameraMovementInput.y;
        cameraHorizontalInput = cameraMovementInput.x;
    }

    public void EnableInput()
    {
        playerInput.Enable();
    }

    public void DisableInput()
    {
        playerInput.Disable();
        playerMovementInput = Vector2.zero;
        cameraMovementInput = Vector2.zero;
    }

    public bool IsActive()
    {
        return playerInput.asset.enabled;
    }

    public String GetInteractionKey()
    {
        return playerInput.PlayerActions.Interaction.GetBindingDisplayString();
    }
}
