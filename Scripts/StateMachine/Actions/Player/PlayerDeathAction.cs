using System.Collections;
using UnityEngine;

public class PlayerDeathAction : StateAction
{
    private Coroutine coroutine;
    private CharacterController characterController;
    private PlayerController playerController;

    public override void Init(StateMachine stateMachine, StateActionSO stateActionSO)
    {
        base.Init(stateMachine, stateActionSO);
        characterController = stateMachine.GetComponent<CharacterController>();
        playerController = stateMachine.GetComponent<PlayerController>();
    }

    public override void OnStateEnter()
    {
        SoundManager.Instance.PlaySFX(Enums.SFX.Death);
        EventManager.Instance.TriggerEvent(Enums.EventType.Player_void_death);
        EventManager.Instance.TriggerEvent(Enums.EventType.Player_void_disableLockon);
        InputManager.Instance.DisableInput();
        coroutine = CoroutineUtil.Instance.StartCoroutine(Death());

        Soul soul = PoolingManager.Instance.Pop<Soul>(Constants.Prefab.NAME_GAME_OBJECTS_SOUL);
        soul.transform.position = playerController.transform.position;
        soul.SetSoulAmount(playerController.PlayerSoul.Soul);
        soul.gameObject.SetActive(true);
        soul.SaveSoulSpawnPos();
    }

    public override void OnStateExit()
    {
        CoroutineUtil.Instance.StopCoroutine(coroutine);
        PopupManager.Instance.Hide<UI_Popup_Death>();
        InputManager.Instance.EnableInput();
    }

    public override void OnUpdate()
    {

    }

    IEnumerator Death()
    {
        PopupManager.Instance.Show<UI_Popup_Death>();
        yield return CoroutineUtil.Instance.WaitForSeconds(5f);

        yield return SceneLoader.Instance.LoadScene(Enums.Scene.Loading);

        PopupManager.Instance.Hide<UI_Popup_Death>();
        ResetPlayerPos();
        InGameManager.Instance.RestartGame();

        yield return CoroutineUtil.Instance.WaitForSeconds(CoroutineUtil.WAIT_SECOND_2);
        yield return SceneLoader.Instance.UnloadScene(Enums.Scene.Loading);
        playerController.Reborn();
    }

    private void ResetPlayerPos()
    {
        SaveData data = DataManager.Instance.CurrentSaveData;
        characterController.enabled = false;
        characterController.transform.position = data.PlayerPosition;
        characterController.enabled = true;
    }
}