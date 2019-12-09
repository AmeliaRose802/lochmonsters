using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HitSnake : IMessage, INetworkMessage
{
    public int otherID;
    public HitSnake(int otherID)
    {
        this.otherID = otherID;
    }

    public byte[] GetMessage()
    {
        List<byte> message = new List<byte>();
        message.AddRange(BitConverter.GetBytes('h'));
        message.AddRange(BitConverter.GetBytes(GameManager.instance.id));
        message.AddRange(BitConverter.GetBytes(otherID));

        return message.ToArray();
    }

    public MessageType GetMessageType()
    {
        return MessageType.HIT_SNAKE;
    }



}