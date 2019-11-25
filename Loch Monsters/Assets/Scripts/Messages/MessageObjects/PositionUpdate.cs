using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class PositionUpdate : IMessage, NetworkMessage
{
    int id;
    long timestamp;
    Vector2 pos;
    Vector2 rotation;

    public PositionUpdate(int id, Vector2 pos, Vector2 rotation)
    {
        this.id = id;
        this.pos = pos;
        this.rotation = rotation;
    }

    //Constructor parses position update from a Byte array passed in
    public PositionUpdate(Byte [] messageBuffer)
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

        pos = new Vector2(posX, posY);
        rotation = new Vector2(dirX, dirY);
    }


    public Byte[] GetMessage()
    {
        
  
        UTF8Encoding utfEncoding = new UTF8Encoding();
        //name = name.PadRight(32, ' ');
        List<byte> packet = new List<byte>();
        packet.AddRange(BitConverter.GetBytes('p'));
        packet.AddRange(BitConverter.GetBytes(id));
        TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
        packet.AddRange(BitConverter.GetBytes(GameManager.instance.gameTime));
        packet.AddRange(BitConverter.GetBytes(pos.x));
        packet.AddRange(BitConverter.GetBytes(pos.y));
        packet.AddRange(BitConverter.GetBytes(rotation.x));
        packet.AddRange(BitConverter.GetBytes(rotation.y));

        return packet.ToArray();
    }

    public MessageType GetMessageType()
    {
        return MessageType.UPDATE_POSITION;
    }

    public int getID()
    {
        return id;
    }

    public Vector2 getPos()
    {
        return pos;
    }

    public Vector2 getDir()
    {
        return rotation;
    }

    public long getTimestamp()
    {
        return timestamp;
    }
}
