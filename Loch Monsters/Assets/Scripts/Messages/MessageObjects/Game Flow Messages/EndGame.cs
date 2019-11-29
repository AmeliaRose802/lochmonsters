using UnityEngine;
using System.Collections;

public class EndGame : IMessage
{
    public MessageType GetMessageType()
    {
        return MessageType.END_GAME;
    }
}