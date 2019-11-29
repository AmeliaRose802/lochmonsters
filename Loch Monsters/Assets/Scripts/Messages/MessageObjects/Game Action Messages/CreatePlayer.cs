using UnityEngine;

public class CreatePlayer : IMessage
{
    public Vector2 pos;
    public Vector2 der;
    public Color color;
    public string name;

    public CreatePlayer(string name, Color color, Vector2 pos, Vector2 der)
    {
        this.color = color;
        this.name = name;
        this.pos = pos;
        this.der = der;
    }

    public MessageType GetMessageType()
    {
        return MessageType.CREATE_PLAYER;
    }
}
