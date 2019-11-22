using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerPosition : IMessage
{
    public Vector2 pos;
    public Vector2 der;

    public SetPlayerPosition(Vector2 pos, Vector2 der)
    {
        this.pos = pos;
        this.der = der;
    }

    public MessageType GetMessageType()
    {
        return MessageType.SET_PLAYER_POSITION;
    }


}
