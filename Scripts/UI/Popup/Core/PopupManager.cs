using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Debug = ccm.Debug;

public class PopupManager : Singleton<PopupManager>
{
    private Dictionary<string, UI_Popup> dic = new Dictionary<string, UI_Popup>();
    private List<UI_Popup> currentPopup = new List<UI_Popup>();

    public void CreatePopup<T>(string name, Transform parent) where T : UI_Popup
    {
        GameObject addressableObj = AddressableManager.Instance.GetPrefab(name);
        CreatePopup<T>(addressableObj, parent);
    }

    public void CreatePopup<T>(GameObject obj, Transform parent) where T : UI_Popup
    {
        string key = typeof(T).Name;
        if (dic.ContainsKey(key))
            return;

        GameObject createObj = Instantiate(obj, parent);

        if (createObj.TryGetComponent(out T popup))
        {
            popup.Init();
            popup.Hide();
            dic.Add(key, popup);
        }
    }

    public T Get<T>() where T : UI_Popup
    {
        UI_Popup popup;
        if (!dic.TryGetValue(typeof(T).Name, out popup))
        {
            Debug.Log("팝업이 존재하지 않습니다.");
            return null;
        }

        return popup as T;
    }

    public bool Show<T>() where T : UI_Popup
    {
        if (!dic.TryGetValue(typeof(T).Name, out UI_Popup popup))
        {
            Debug.Log("팝업이 존재하지 않습니다.");
            return false;
        }

        popup.transform.SetAsLastSibling();
        popup.Show();
        currentPopup.Add(popup);
        return true;
    }

    public void Show<T>(Action callback) where T : UI_Popup
    {
        if (Show<T>())
        {
            callback?.Invoke();
        }
    }

    public void Hide<T>() where T : UI_Popup
    {
        if (currentPopup.Count <= 0)
            return;

        for (int i = 0; i < currentPopup.Count; i++)
        {
            if (currentPopup[i] is T)
            {
                currentPopup[i].Hide();
                currentPopup.RemoveAt(i);
                break;
            }
        }
    }

    public void HideLast()
    {
        if (currentPopup.Count <= 0)
            return;

        currentPopup.Last().Hide();
        currentPopup.RemoveAt(currentPopup.Count - 1);
    }

    public bool IsEmpty()
    {
        return currentPopup.Count == 0;
    }

    public bool IsTop<T>() where T : UI_Popup
    {
        if (IsEmpty()) return false;

        return currentPopup.Last() is T;
    }

    public bool IsShow<T>() where T : UI_Popup
    {
        for (int i = 0; i < currentPopup.Count; i++)
        {
            if (currentPopup[i] is T)
                return true;
        }

        return false;
    }

    public void Clear()
    {
        dic.Clear();
        currentPopup.Clear();
    }
}
