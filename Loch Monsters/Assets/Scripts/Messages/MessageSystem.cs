using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MessageType{
    START_GAME,
    SET_SNAKE_POSITION,
    SET_PLAYER_POSITION,
    UPDATE_POSITION,
    EXIT_GAME,
    CONNECT,
    SPAWN_NON_PLAYER_SNAKE,
    REQUEST_CLOCK_SYNC,
    TERMINATION_MESSAGE,
    NEW_FOOD,
    ATE_FOOD,
    FOOD_EATEN
}

public class MessageSystem : MonoBehaviour
{
    Dictionary<MessageType, List<IMessageListener>> listeners;

    public void Awake()
    {
        listeners = new Dictionary<MessageType, List<IMessageListener>>();
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
