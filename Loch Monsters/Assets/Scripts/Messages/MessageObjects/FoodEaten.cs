using UnityEngine;
using System.Collections;
using System;

public class FoodEaten : IMessage
{
    public int foodID;
    public int snakeID;

    public FoodEaten(byte[] buffer)
    {
        int index = 0;
        foodID = BitConverter.ToInt32(buffer, index);
        index += 4;
        snakeID = BitConverter.ToInt32(buffer, index); 
    }

    public MessageType GetMessageType()
    {
        return MessageType.FOOD_EATEN;
    }
}
