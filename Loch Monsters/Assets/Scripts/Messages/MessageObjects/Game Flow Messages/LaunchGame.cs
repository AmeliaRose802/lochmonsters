using UnityEngine;
using System.Collections;

public class LaunchGame : IMessage
{
    public readonly string playerName;
    public readonly Color playerColor;
    public readonly string serverIP;
    public readonly int serverPort;

    public LaunchGame(string serverIP, int serverPort, string name, Color color)
    {
        this.serverIP = serverIP;
        this.serverPort = serverPort;
        playerName = name;
        playerColor = color;
    }

    public MessageType GetMessageType()
    {
        return MessageType.CONNECT_GAME;
    }
}
