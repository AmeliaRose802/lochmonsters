using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class ConnectMessage
{
    byte[] mType = BitConverter.GetBytes('c');

    System.Random random = new System.Random();

    byte[] colorR;
    byte[] colorB;
    byte[] colorG;
    byte[] name;


    public ConnectMessage(Color color, string playerName)
    {
        playerName = playerName.PadRight(32, ' ');


        Debug.Log(playerName);


        UTF8Encoding utfEncoding = new UTF8Encoding();
        colorR = BitConverter.GetBytes((short)(color.r * 255)); //Floats have been acting funky so I'm sending them as shorts instead
        colorG = BitConverter.GetBytes((short)(color.g * 255));
        colorB = BitConverter.GetBytes((short)(color.b * 255));


        name = utfEncoding.GetBytes(playerName);

        //Need it in network byte order (bit endian)
        if (BitConverter.IsLittleEndian)
        {
            //Array.Reverse(colorR);
        }
    }

    public void Send(NetworkStream tcpStream)
    {
        tcpStream.Write(mType, 0, mType.Length);
        tcpStream.Write(colorR, 0, colorR.Length);
        tcpStream.Write(colorG, 0, colorG.Length);
        tcpStream.Write(colorB, 0, colorB.Length);
        tcpStream.Write(name, 0, name.Length);
    }
}
