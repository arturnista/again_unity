using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{

    public delegate void ListenerCallback(EventData eventData);
    private static Dictionary<EventType, ListenerCallback> events = new Dictionary<EventType, ListenerCallback>();

    private static EventManager s_Instance;
    public static EventManager Instance
    {
        get
        {
            if(s_Instance == null)
            {
                s_Instance = new EventManager();
            }

            return s_Instance;
        }
    }

    public enum EventType
    {
        None = 0,
        
        PlayerTakeRedKey = 100,
        PlayerTakeBlueKey = 101,
        PlayerTakeYellowKey = 102,

        PlayerTakeDamage = 200,
        PlayerDeath = 201,

        GameWin = 400,

        GamePause = 401,
        GameResume = 402,
    }

    private EventManager()
    {
        
    }

    public void Void(EventData eventData)
    {

    }

    public void AddEventListener(EventType eventType, ListenerCallback callback)
    {
        if(!events.ContainsKey(eventType))
        {
            events[eventType] = Void;
        }

        events[eventType] += callback;
    }

    public void RemoveEventListener(EventType eventType, ListenerCallback callback)
    {
        if(!events.ContainsKey(eventType))
        {
            return;
        }

        events[eventType] -= callback;
    }

    public void DispatchEvent(EventType eventType)
    {
        if(!events.ContainsKey(eventType))
        {
            return;
        }
        
        EventData eventData = new EventData(null);
        DispatchEvent(eventType, eventData);
    }

    public void DispatchEvent(EventType eventType, GameObject dispatcher)
    {
        if(!events.ContainsKey(eventType))
        {
            return;
        }
        
        EventData eventData = new EventData(dispatcher);
        DispatchEvent(eventType, eventData);
    }

    public void DispatchEvent(EventType eventType, EventData eventData)
    {
        if(!events.ContainsKey(eventType))
        {
            return;
        }

        events[eventType]?.Invoke(eventData);
    }
    
}
