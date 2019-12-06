using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    /*
     * Objects must unsubscribe from the message system when they are destroyed otherwise pain and suffering
     * Sometimes I forget so this will clean up any I missed
     * */
    public void CleanNull()
    {
        foreach(List<IMessageListener> list in listeners.Values)
        {
            foreach(IMessageListener listener in list)
            {
                if(listener == null)
                {
                    list.Remove(listener);
                    Debug.Log("Found a very naughty object that did not clean up after itself");
                }
            }
        }
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        CleanNull();
    }
}
