using UnityEngine;
using System.Collections;

public enum EndType
{
    END_WIN,
    END_LOSE,
    END_SERVER_ERROR  
}


public class EndGame : IMessage
{
    public readonly EndType endType = EndType.END_SERVER_ERROR;
    public readonly string message;
    public readonly string[] endMessages = { "You Win", "You Lose", "Network Disconnect" };

    readonly MessageType messageType = MessageType.END_GAME;
    

    public EndGame(EndType endType, MessageType messageType, string message)
    {
        this.endType = endType;
        this.message = message;
        this.messageType = messageType;
    }

    public MessageType GetMessageType()
    {
        return messageType;
    }
}