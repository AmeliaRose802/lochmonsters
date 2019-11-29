using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class PositionUpdate : IMessage, INetworkMessage
{
    public readonly int id;
    public readonly long timestamp;
    public Vector2 position;
    public Vector2 direction;

    MessageType type = MessageType.UPDATE_NP_POSITION;

    public PositionUpdate(int id, Vector2 pos, Vector2 direction, MessageType type)
    {
        this.id = id;
        position = pos;
        this.direction = direction;
        this.type = type;
    }

    //Constructor parses position update from a Byte array passed in
    public PositionUpdate(byte [] messageBuffer)
    {
        int index = 0;
        index += 2;
        id = BitConverter.ToInt32(messageBuffer, index);
        index += 4;
        timestamp = BitConverter.ToInt32(messageBuffer, index);
        index += 8;
        float posX = BitConverter.ToSingle(messageBuffer, index);
        index += 4;
        float posY = BitConverter.ToSingle(messageBuffer, index);
        index += 4;
        float dirX = BitConverter.ToSingle(messageBuffer, index);
        index += 4;
        float dirY = BitConverter.ToSingle(messageBuffer, index);

        position = new Vector2(posX, posY);
        direction = new Vector2(dirX, dirY);
    }


    public byte[] GetMessage()
    {
        UTF8Encoding utfEncoding = new UTF8Encoding();
        List<byte> packet = new List<byte>();
        packet.AddRange(BitConverter.GetBytes('p'));
        packet.AddRange(BitConverter.GetBytes(id));
        TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
        packet.AddRange(BitConverter.GetBytes(GameManager.instance.gameTime));
        packet.AddRange(BitConverter.GetBytes(position.x));
        packet.AddRange(BitConverter.GetBytes(position.y));
        packet.AddRange(BitConverter.GetBytes(direction.x));
        packet.AddRange(BitConverter.GetBytes(direction.y));

        return packet.ToArray();
    }

    public MessageType GetMessageType()
    {
        return type;
    }

}
