using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerAteFood : INetworkMessage, IMessage
{
    public int foodID;

    public PlayerAteFood(int id)
    {
        foodID = id;
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
        return MessageType.ATE_FOOD;
    }
}
