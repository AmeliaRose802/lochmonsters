using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class ConnectMessage : IMessage
{
    UTF8Encoding utfEncoding = new UTF8Encoding();

    short colorR;
    short colorB;
    short colorG;
    string name;

    public MessageType GetMessageType()
    {
        return MessageType.CONNECT;
    }

    public ConnectMessage(Color color, string playerName)
    {
        name = playerName.PadRight(32, ' ');
   
        UTF8Encoding utfEncoding = new UTF8Encoding();
        colorR = (short)(color.r * 255);
        colorG = (short)(color.g * 255);
        colorB = (short)(color.b * 255);
    }

    public Byte[] GetMessage()
    {
        List<Byte> message = new List<Byte>();
        message.AddRange(BitConverter.GetBytes('c'));
        message.AddRange(BitConverter.GetBytes(colorR));
        message.AddRange(BitConverter.GetBytes(colorG));
        message.AddRange(BitConverter.GetBytes(colorB));
        message.AddRange(utfEncoding.GetBytes(name));

        return message.ToArray();
    }
}
