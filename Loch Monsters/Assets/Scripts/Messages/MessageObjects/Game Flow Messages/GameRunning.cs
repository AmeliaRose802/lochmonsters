using UnityEngine;
using System.Collections;

public class GameRunning : IMessage
{
    public MessageType GetMessageType()
    {
        return MessageType.GAME_RUNNING;
    }
}
