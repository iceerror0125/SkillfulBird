using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;


public class Message
{
    public EventType eventType;
    public object param;
    public object returnValue;

    public Message()
    {
        eventType = EventType.None;
        param = null;
        returnValue = null;
    }
    public Message(EventType eventType, object param = null)
    {
        this.eventType = eventType;
        this.param = param;
    }
    public Message(object param)
    {
        this.param = param;
    }
}

public class Observer : SingletonMono<Observer>
{
    private Dictionary<EventType, List<Action<Message>>> observer = new();

    public void Subscribe(EventType type, Action<Message> callBack)
    {
        if (observer.ContainsKey(type))
        {
            observer[type].Add(callBack);
        }
        else
        {
            observer.Add(type, new List<Action<Message>> { callBack });
        }
    }
    public void UnSubscribe(EventType type, Action<Message> callBack)
    {
        if (observer.ContainsKey(type))
        {
            if (observer[type].Count == 1)
            {
                observer.Remove(type);
            }
            else
            {
                observer[type].Remove(callBack);
            }
        }
        else
        {
            Debug.LogError("Unsubscribe fail: Event type doesn't exist");
        }
    }
    public void Announce(Message message)
    {
        EventType type = message.eventType;

        if (observer.ContainsKey(type))
        {
            foreach (Action<Message> action in observer[type])
            {
                action?.Invoke(message);
            }
        }
        else
        {
            Debug.LogError("Announce fail: Event type doesn't exist");
        }
    }


    public object GetCallBack(Message callBackMsg)
    {
        Announce(callBackMsg);
        Debug.Log($"call back: {callBackMsg.returnValue}");

        return callBackMsg.returnValue;
    }
}
