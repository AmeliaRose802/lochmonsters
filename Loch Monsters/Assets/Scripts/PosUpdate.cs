using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PosUpdate : MonoBehaviour
{
    string name;
    int xPos;
    int yPos;

    PosUpdate(string name, int xPos, int yPos)
    {
        this.name = name;
        this.xPos = xPos;
        this.yPos = yPos;
    }

    public void Send(UdpClient udpClient)
    {
       
    }
}
