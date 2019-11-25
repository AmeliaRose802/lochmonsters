using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSnake : IMessage
{
    public SnakeData snake;

    public SpawnSnake(SnakeData snake)
    {
        this.snake = snake;
    }

    public SpawnSnake(byte [] snake)
    {
        this.snake = new SnakeData(snake);
    }

    public MessageType GetMessageType()
    {
        return MessageType.SPAWN_NON_PLAYER_SNAKE;
    }


}
