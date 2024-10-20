using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TitleController : MonoBehaviour
{
    [SerializeField]
    private Transform popupRoot;

    private void Awake()
    {
        Time.timeScale = 1.0f;

#if UNITY_EDITOR
        if (!Initialization.Instance.IsDone)
        {
            StartCoroutine(EditorPlayInit());
            return;
        }
#endif

        Init();
    }

    private void Init()
    {
        SoundManager.Instance.PlayBGMWithFadeIn(Enums.BGM.Title);
        DataManager.Instance.LoadAll();

        PopupManager.Instance.Clear();

        PopupManager.Instance.CreatePopup<UI_Popup_Slots>(Constants.Prefab.NAME_UI_POPUP_SLOTS, popupRoot);
        PopupManager.Instance.CreatePopup<UI_Popup_Setting>(Constants.Prefab.NAME_UI_POPUP_SETTING, popupRoot);
    }

#if UNITY_EDITOR

    private IEnumerator EditorPlayInit()
    {
        popupRoot.gameObject.SetActive(false);
        yield return new WaitUntil(() => Initialization.Instance.IsDone);
        Init();

        popupRoot.gameObject.SetActive(true);
    }

#endif
}
