using System.Collections.Generic;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    public static MessageSystem instance;

    Dictionary<MessageType, List<IMessageListener>> listeners;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        listeners = new Dictionary<MessageType, List<IMessageListener>>();
        DontDestroyOnLoad(this.gameObject);
    }

    public void Subscribe(MessageType type, IMessageListener listener)
    {
        if (listeners.ContainsKey(type))
        {
            listeners[type].Add(listener);
        }
        else
        {
            var listenersForKey = new List<IMessageListener>();
            listenersForKey.Add(listener);
            listeners.Add(type,listenersForKey);
        }
        
        
    }

    public void Unsubscribe(MessageType type, IMessageListener listener)
    {
        if (listeners.ContainsKey(type))
        {
            listeners[type].Remove(listener);
        }
    }

    public void DispatchMessage(IMessage message)
    {
        if (listeners.ContainsKey(message.GetMessageType()))
        {
            foreach (var listener in listeners[message.GetMessageType()])
            {
                listener.Receive(message);
            }
        }
        
    }
}
