using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class ConnectMessage : INetworkMessage
{
    UTF8Encoding utfEncoding = new UTF8Encoding();

    readonly short colorR;
    readonly short colorB;
    readonly short colorG;
    readonly string name;

    public ConnectMessage(Color color, string playerName)
    {
        name = playerName.PadRight(32, ' ');
   
        UTF8Encoding utfEncoding = new UTF8Encoding();
        colorR = (short)(color.r * 255);
        colorG = (short)(color.g * 255);
        colorB = (short)(color.b * 255);
    }

    public byte[] GetMessage()
    {
        List<byte> message = new List<byte>();
        message.AddRange(BitConverter.GetBytes('c'));
        message.AddRange(BitConverter.GetBytes(colorR));
        message.AddRange(BitConverter.GetBytes(colorG));
        message.AddRange(BitConverter.GetBytes(colorB));
        message.AddRange(utfEncoding.GetBytes(name));

        return message.ToArray();
    }
}
