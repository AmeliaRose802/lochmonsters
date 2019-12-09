using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HitBySnake : IMessage, INetworkMessage
{
    public int otherID;
    public HitBySnake(int otherID)
    {
        this.otherID = otherID;
    }

    public byte[] GetMessage()
    {
        List<byte> message = new List<byte>();
        message.AddRange(BitConverter.GetBytes('r'));
        message.AddRange(BitConverter.GetBytes(GameManager.instance.id));
        message.AddRange(BitConverter.GetBytes(otherID));

        return message.ToArray();
    }

    public MessageType GetMessageType()
    {
        return MessageType.HIT_BY;
    }



}
