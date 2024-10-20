using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UtilManager : Singleton<UtilManager>
{
   public void AddButtonEventTrigger(GameObject obj, EventTriggerType type, Action action)
    {
        EventTrigger et = obj.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();

        entry.eventID = type;
        entry.callback.AddListener((e) => action?.Invoke());
        
        et.triggers.Add(entry);
    }

    public void AddButtonEnterSound(GameObject obj)
    {
        AddButtonEventTrigger(obj, EventTriggerType.PointerEnter, () => SoundManager.Instance.PlaySFX(Enums.SFX.Hover));
    }
}
