using System;
using System.Collections.Generic;
using UnityEngine;


public class EventManager : MonoBehaviour
{
    private readonly Dictionary<string, Action<object[]>> _eventsDictionary = new();
    public static EventManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void RegisterListener(string eventName, Action<object[]> listener)
    {
        if (_eventsDictionary.TryGetValue(eventName, out Action<object[]> thisEvent))
        {
            thisEvent += listener;
            _eventsDictionary[eventName] = thisEvent;
        }
        else
        {
            _eventsDictionary.Add(eventName, listener);
        }
    }

    public void UnregisterListener(string eventName, Action<object[]> listener)
    {
        if (_eventsDictionary.TryGetValue(eventName, out Action<object[]> thisEvent))
        {
            thisEvent -= listener;
            if (thisEvent == null) _eventsDictionary.Remove(eventName);
            else _eventsDictionary[eventName] = thisEvent;
        }
    }

    public void BroadcastEvent(string eventName, params object[] parameters)
    {
        if (_eventsDictionary.TryGetValue(eventName, out Action<object[]> thisEvent))
        {
            thisEvent.Invoke(parameters);
        }
        else
        {
            Debug.LogWarning($"Event '{eventName}' not found.");
        }
    }
}