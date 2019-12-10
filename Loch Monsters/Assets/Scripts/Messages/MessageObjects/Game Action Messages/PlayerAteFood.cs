using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerAteFood : INetworkMessage, IMessage
{
    public int foodID;
    MessageType type = MessageType.ATE_FOOD;

    public PlayerAteFood(int id)
    {
        foodID = id;
    }

    public PlayerAteFood(int id, MessageType type)
    {
        foodID = id;
        this.type = type;
    }

    public byte[] GetMessage()
    {
        List<byte> message = new List<byte>();
        message.AddRange(BitConverter.GetBytes('a'));
        message.AddRange(BitConverter.GetBytes(foodID));
        message.AddRange(BitConverter.GetBytes(GameManager.instance.id));
        return message.ToArray();
    }

    public MessageType GetMessageType()
    {
        return type;
    }
}
