using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * All classes wishing to receve messages from the message system must implement this class
 * */
public interface IMessageListener
{
    void Receive(IMessage message);
}
