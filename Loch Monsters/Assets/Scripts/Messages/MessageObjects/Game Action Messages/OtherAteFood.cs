using UnityEngine;
using System.Collections;
using System;

public class OtherAteFood : IMessage
{
    public int foodID;
    public int snakeID;

    public OtherAteFood(byte[] buffer)
    {
        int index = 0;
        foodID = BitConverter.ToInt32(buffer, index);
        index += 4;
        snakeID = BitConverter.ToInt32(buffer, index);
        Debug.Log("Food: " + foodID + " eaten by " + snakeID);
    }

    public MessageType GetMessageType()
    {
        return MessageType.FOOD_EATEN;
    }
}