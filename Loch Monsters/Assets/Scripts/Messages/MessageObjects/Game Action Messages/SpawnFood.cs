using UnityEngine;
using System;

public class SpawnFood : IMessage
{
    public int id;
    public Vector2 pos;

    public SpawnFood(byte [] buffer)
    {
        int index = 0;
        id = BitConverter.ToInt32(buffer, index);
        index += 4;
        float posX = BitConverter.ToSingle(buffer, index);
        index += 4;
        float posY = BitConverter.ToSingle(buffer, index);

        pos = new Vector2(posX, posY);
    }

    public SpawnFood(FoodData food)
    {
        id = food.id;
        pos = food.pos;
    }

    public MessageType GetMessageType()
    {
        return MessageType.SPAWN_NEW_FOOD;
    }
}