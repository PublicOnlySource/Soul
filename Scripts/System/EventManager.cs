using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EventManager : Singleton<EventManager>
{
    private Dictionary<Enums.EventType, List<Delegate>> eventDictionary = new Dictionary<Enums.EventType, List<Delegate>>();

    private void OnDestroy()
    {
       Dispose();
    }

    public void AddListener<T>(Enums.EventType eventType, Action<T> listener)
    {
        if (!eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] = new List<Delegate>();
        }
        eventDictionary[eventType].Add(listener);
    }

    public void AddListener(Enums.EventType eventType, Action listener)
    {
        if (!eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] = new List<Delegate>();
        }
        eventDictionary[eventType].Add(listener);
    }

    public void RemoveListener<T>(Enums.EventType eventType, Action<T> listener)
    {
        if (!eventDictionary.ContainsKey(eventType))
            return;

        eventDictionary[eventType].Remove(listener);

        if (eventDictionary[eventType].Count == 0)
        {
            eventDictionary.Remove(eventType);
        }

    }

    public void RemoveListener(Enums.EventType eventType, Action listener)
    {
        if (!eventDictionary.ContainsKey(eventType))
            return;

        eventDictionary[eventType].Remove(listener);

        if (eventDictionary[eventType].Count == 0)
        {
            eventDictionary.Remove(eventType);
        }
    }

    public void TriggerEvent<T>(Enums.EventType eventType, T data)
    {
        if (!eventDictionary.TryGetValue(eventType, out var delegates))
            return;

        foreach (var d in delegates.ToList())
        {
            if (d is Action<T> action)
            {
                action(data);
            }
        }
    }

    public void TriggerEvent(Enums.EventType eventType)
    {
        if (!eventDictionary.TryGetValue(eventType, out var delegates))
            return;

        foreach (var d in delegates.ToList())
        {
            if (d is Action action)
            {
                action();
            }
        }
    }

    public void Dispose()
    {
        eventDictionary.Clear();
    }
}
