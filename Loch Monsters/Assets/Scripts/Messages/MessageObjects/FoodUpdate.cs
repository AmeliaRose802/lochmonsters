using UnityEngine;
using System.Collections;
using System;

public class FoodUpdate : IMessage
{
    public int id;
    public Vector2 pos;

    public FoodUpdate(byte [] buffer)
    {
        int index = 0;
        id = BitConverter.ToInt32(buffer, index);
        index += 4;
        float posX = BitConverter.ToSingle(buffer, index);
        index += 4;
        float posY = BitConverter.ToSingle(buffer, index);

        pos = new Vector2(posX, posY);
    }

    public FoodUpdate(FoodData food)
    {
        id = food.id;
        pos = food.pos;
    }

    public MessageType GetMessageType()
    {
        return MessageType.NEW_FOOD;
    }
}