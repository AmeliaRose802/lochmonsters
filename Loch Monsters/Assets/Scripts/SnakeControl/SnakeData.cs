using System;
using System.Collections.Generic;
using UnityEngine;

public class SnakeData
{
    public int id;
    public string name;
    public short length;
    public Color color;
    public Vector2 pos;
    public Vector2 dir;
    public long lastUpdateTime = 0;

    public SnakeData(int id, string name, short length, Color color, Vector2 pos, Vector2 dir)
    {
        this.id = id;
        this.name = name;
        this.length = length;
        this.color = color;
        this.pos = pos;
        this.dir = dir;
    }


    public SnakeData(byte[] snakeDataBytes)
    {
        int index = 0;
        this.id = BitConverter.ToInt32(snakeDataBytes, index);


        index += 4;
        byte[] nameBuffer = new List<Byte>(snakeDataBytes).GetRange(index, 32).ToArray();

        name = System.Text.Encoding.UTF8.GetString(nameBuffer).Trim();
        index += 32;

        length = BitConverter.ToInt16(snakeDataBytes, index);
        index += 2;
        short colorR = BitConverter.ToInt16(snakeDataBytes, index);
        index += 2;
        short colorG = BitConverter.ToInt16(snakeDataBytes, index);
        index += 2;
        short colorB = BitConverter.ToInt16(snakeDataBytes, index);
        index += 2;

        float xPos = BitConverter.ToSingle(snakeDataBytes, index);
        index += 4;
        float yPos = BitConverter.ToSingle(snakeDataBytes, index);
        index += 4;
        float xDir = BitConverter.ToSingle(snakeDataBytes, index);
        index += 4;
        float yDir = BitConverter.ToSingle(snakeDataBytes, index);


        color = new Color(((float)colorR / 255f), ((float)colorG / 255f), ((float)colorB / 255f));
        pos = new Vector2(xPos, yPos);
        dir = new Vector2(xDir, yDir);
    }
}
