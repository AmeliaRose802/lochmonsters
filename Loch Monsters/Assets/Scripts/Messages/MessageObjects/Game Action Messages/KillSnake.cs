using UnityEngine;
using System.Collections;
using System;

public class KillSnake : IMessage
{
    public int id;
    public KillSnake(byte[] buffer)
    {
        id = BitConverter.ToInt32(buffer, 0);
    }

    public MessageType GetMessageType()
    {
        return MessageType.KILL_SNAKE;
    }


  
}
