using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPSnake : IMessage
{
    public SnakeData snake;

    public SpawnNPSnake(SnakeData snake)
    {
        this.snake = snake;
    }

    public SpawnNPSnake(byte [] snake)
    {
        this.snake = new SnakeData(snake);
    }

    public MessageType GetMessageType()
    {
        return MessageType.SPAWN_NON_PLAYER_SNAKE;
    }
}
