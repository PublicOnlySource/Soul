using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class UI_Title : MonoBehaviour
{
    [SerializeField]
    private Button gameStartButton;
    [SerializeField]
    private Button settingButton;
    [SerializeField]
    private Button exitButton;

    private void Start()
    {
        AddButtonEvent();
    }

    private void AddButtonEvent()
    {
        gameStartButton.onClick.AddListener(NewGame);
        settingButton.onClick.AddListener(Setting);
        exitButton.onClick.AddListener(ExitGame);

        UtilManager.Instance.AddButtonEnterSound(gameStartButton.gameObject);
        UtilManager.Instance.AddButtonEnterSound(settingButton.gameObject);
        UtilManager.Instance.AddButtonEnterSound(exitButton.gameObject);
    }

    private void NewGame()
    {
        PopupManager.Instance.Show<UI_Popup_Slots>();
    }

    private void Setting()
    {
        PopupManager.Instance.Show<UI_Popup_Setting>();
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void EnableButtons()
    {
        gameStartButton.interactable = true;
        settingButton.interactable = true;
        exitButton.interactable = true;
    }

    public void DisableButtons()
    {
        gameStartButton.interactable = false;
        settingButton.interactable = false;
        exitButton.interactable = false;
    }
}
