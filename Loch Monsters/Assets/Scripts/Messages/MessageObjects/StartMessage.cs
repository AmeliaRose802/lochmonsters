using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMessage : IMessage
{
    public int id;
    public Vector2 startPos;
    public Vector2 startDir;
    public List<SnakeData> otherSnakes;


    public StartMessage(int x, int y)
    {
        otherSnakes = new List<SnakeData>();
        startPos = new Vector2(x, y);
        startDir = new Vector2(0, 0);
    }

    public StartMessage(byte [] message, long latency)
    {
        otherSnakes = new List<SnakeData>();
        int index = 0;
        id = BitConverter.ToInt32(message, index);
        index += 4;
        int xPos = BitConverter.ToInt32(message, index);
        index += 4;
        int yPos = BitConverter.ToInt32(message, index);
        index += 4;
        long time = BitConverter.ToInt64(message, index);
        index += 8;
        startDir = new Vector2(0, 0);

        //Logically this should be being sent in the start game message but that takes time and I don't want to get more out of date
        GameManager.instance.gameTime = time + latency;
        GameManager.instance.updateClock = true;
    }

    public MessageType GetMessageType()
    {
        return MessageType.START_GAME;
    }

}
