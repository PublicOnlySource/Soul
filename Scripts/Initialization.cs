using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Initialization : Singleton<Initialization>
{
    [SerializeField]
    private Transform parent;

    private bool isDone = false;

    public bool IsDone { get => isDone; }

    private void Awake()
    {
        StartCoroutine(Process());       
    }
    
   private IEnumerator Process()
    {
        bool isInitScene = SceneLoader.Instance.IsActiveScene(Enums.Scene.Initialization);

        if (isInitScene)
        {
            CreatePopup();
        }

        AddressableManager.Instance.Init(!isInitScene);
        yield return new WaitUntil(() => AddressableManager.Instance.IsCompleteLoad);

        Init();       

        if (isInitScene)
        {
            yield return SceneLoader.Instance.LoadScene(Enums.Scene.Title);
            yield return SceneLoader.Instance.UnloadScene(Enums.Scene.Initialization);
        }
    }

    private void CreatePopup()
    {
        UI_Popup_Update popup = ResourceManager.Instance.Load<UI_Popup_Update>(Constants.Prefab.NAME_UI_POPUP_Update);
        PopupManager.Instance.CreatePopup<UI_Popup_Update>(popup.gameObject, parent);
    }

    private void Init()
    {
        DataManager.Instance.Init();
        SoundManager.Instance.Init();
        isDone = true;
    }

}
