using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InGameManager : Singleton<InGameManager>
{
    [Header("UI")]
    [SerializeField]
    private Transform popupRoot;

    private PlayerController playerController;
    private bool isShowMenu = false;
    private bool isNewGame = false;
    private float playtime = 0f;

    public PlayerController PlayerController { get => playerController; }


    private void OnEnable()
    {
        EventManager.Instance.AddListener(Enums.EventType.Data_void_applySaveData, ApplyData);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener(Enums.EventType.Data_void_applySaveData, ApplyData);       
    }

    private void Awake()
    {
        HideMouse();
        playerController = FindAnyObjectByType<PlayerController>();

#if UNITY_EDITOR
        if (!Initialization.Instance.IsDone)
        {
            StartCoroutine(EditorDirectPlayInit());
            return;
        }
#endif     

        AwakeInit();
    }

    private void Start()
    {
#if UNITY_EDITOR
        if (Initialization.Instance.IsDone)
        {
            StartInit();
            return;
        }
#endif     

        StartInit();
    }

    private void Update()
    {
        playtime += Time.fixedUnscaledDeltaTime;

        if (!isShowMenu && InputManager.Instance.IsMenu)
        {
            isShowMenu = true;
            SetActiveGamePopup();
        }
        else if (isShowMenu && !InputManager.Instance.IsMenu)
        {
            isShowMenu = false;
        }
    }

    private void AwakeInit()
    {
        SoundManager.Instance.PlayBGMWithFadeIn(Enums.BGM.Main);
        EffectManager.Instance.Init();

        InitPool();

        InitPopup();
        FollowCameraComponent.Instance.ResetRotationByPlayerView();

    }

    private void StartInit()
    {
        InitData();
        EventManager.Instance.TriggerEvent(Enums.EventType.Monster_void_spawn);

        if (isNewGame)
        {
            EventManager.Instance.TriggerEvent(Enums.EventType.Data_void_writeSaveData);
            DataManager.Instance.SaveGame();
        }
        else
        {
            EventManager.Instance.TriggerEvent(Enums.EventType.Data_void_applySaveData);
        }
    }

    private void InitPopup()
    {
        PopupManager.Instance.Clear();
        PopupManager.Instance.CreatePopup<UI_Popup_Death>(Constants.Prefab.NAME_UI_POPUP_DEATH, popupRoot);
        PopupManager.Instance.CreatePopup<UI_Popup_Game>(Constants.Prefab.NAME_UI_POPUP_GAME, popupRoot);
        PopupManager.Instance.CreatePopup<UI_Popup_KeySign>(Constants.Prefab.NAME_UI_POPUP_KEYSIGN, popupRoot);
        PopupManager.Instance.CreatePopup<UI_Popup_Rest>(Constants.Prefab.NAME_UI_POPUP_REST, popupRoot);
        PopupManager.Instance.CreatePopup<UI_Popup_Upgrade>(Constants.Prefab.NAME_UI_POPUP_UPGRADE, popupRoot);
        PopupManager.Instance.CreatePopup<UI_Popup_Setting>(Constants.Prefab.NAME_UI_POPUP_SETTING, popupRoot);
        PopupManager.Instance.CreatePopup<UI_Popup_Inventory>(Constants.Prefab.NAME_UI_POPUP_INVENTORY, popupRoot);
        PopupManager.Instance.CreatePopup<UI_Popup_Exit>(Constants.Prefab.NAME_UI_POPUP_EXIT, popupRoot);
        PopupManager.Instance.CreatePopup<UI_Popup_Destroyed>(Constants.Prefab.NAME_UI_POPUP_DESTROYED, popupRoot);
        PopupManager.Instance.CreatePopup<UI_Popup_DropItemInfo>(Constants.Prefab.NAME_UI_POPUP_DROPITEMINFO, popupRoot);

    }

    private void InitData()
    {
        SaveData data = DataManager.Instance.CurrentSaveData;
  
        if (data.PlayerPosition == Vector3.zero && data.Inventory.Count == 0)
        {
            PlayerInventory inven = playerController.PlayerInventory;
            inven.Add(Constants.Item.EQUIPMENT_DEFAULT_SWORD);
            inven.Add(Constants.Item.CONSUMABLE_POTION, 3);
            inven.Use(0);
            isNewGame = true;           
        }
    }

    private void InitPool()
    {
        PoolingManager.Instance.CreatePool(Constants.Prefab.NAME_GAME_MONSTERS_HUMAN, 3);
        PoolingManager.Instance.CreatePool(Constants.Prefab.NAME_GAME_OBJECTS_SOUL, 1);
        PoolingManager.Instance.CreatePool(Constants.Item.CONSUMABLE_POTION.ToString(), 1);
    }

    private void SetActiveGamePopup()
    {
        if (playerController.IsRest)
            return;

        if (PopupManager.Instance.IsShow<UI_Popup_Game>())
        {
            if (!PopupManager.Instance.IsTop<UI_Popup_Game>())
            {
                PopupManager.Instance.HideLast();
                return;
            }

            PopupManager.Instance.Hide<UI_Popup_Game>();
            HideMouse();
            GameResume();
            return;
        }
        else
        {
            PopupManager.Instance.Show<UI_Popup_Game>();
            ShowMouse();
            GamePause();
        }        
    }

    private void ApplyData()
    {
        SaveData data = DataManager.Instance.CurrentSaveData;
        playtime += data.PlayTime;

        if (data.SoulDropPosition != Vector3.zero)
        {
            Soul soul = PoolingManager.Instance.Pop<Soul>(Constants.Prefab.NAME_GAME_OBJECTS_SOUL);
            soul.transform.position = data.SoulDropPosition;
            soul.SetSoulAmount(data.SoulDropAmount);
            soul.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        EventManager.Instance.TriggerEvent(Enums.EventType.Monster_void_allDestory);
        EventManager.Instance.TriggerEvent(Enums.EventType.Monster_void_spawn);
    }

    public void GamePause()
    {
        Time.timeScale = 0f;
        InputManager.Instance.DisableInput();
    }

    public void GameResume()
    {
        Time.timeScale = 1f;
        InputManager.Instance.EnableInput();
    }

    public void Dispose()
    {
        PoolingManager.Instance.Delete(Constants.Prefab.NAME_GAME_MONSTERS_HUMAN);
        PoolingManager.Instance.Delete(Constants.Prefab.NAME_GAME_OBJECTS_SOUL);

        Destroy(FollowCameraComponent.Instance.gameObject);
        Destroy(EffectManager.Instance.gameObject);
        Destroy(CoroutineUtil.Instance.gameObject);
        Destroy(InputManager.Instance.gameObject);
        Destroy(EventManager.Instance.gameObject);
        Destroy(this.gameObject);       
    }

    public void WritePlayTime()
    {
        DataManager.Instance.CurrentSaveData.WritePlayTime(playtime);
    }

    public void HideMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShowMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

#if UNITY_EDITOR

    IEnumerator EditorDirectPlayInit()
    {
        yield return new WaitUntil(() => Initialization.Instance.IsDone);

        if (DataManager.Instance.CurrentSaveData == null)
        {
            DataManager.Instance.Init();
            DataManager.Instance.LoadAll();
            DataManager.Instance.LoadGame(0);
        }

        AwakeInit();
        StartInit();
    }

#endif
}
