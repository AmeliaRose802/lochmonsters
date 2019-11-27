using UnityEngine;
using System.Collections;
using System;

public struct FoodData
{
    public int id;
    public Vector2 pos;

    public FoodData(byte [] buffer)
    {
        int index = 0;
        id = BitConverter.ToInt32(buffer, index);
        index += 4;
        float posX = BitConverter.ToSingle(buffer, index);
        index += 4;
        float posY = BitConverter.ToSingle(buffer, index);

        pos = new Vector2(posX, posY);
    }
}
