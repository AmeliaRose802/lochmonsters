using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

public class NetworkManagerScript : MonoBehaviour
{
    [HideInInspector]
    public TCPManager tcpManager;

    [HideInInspector]
    public UDPManager udpManager;
    

    public void Awake()
    {
        tcpManager = GetComponent<TCPManager>();
        udpManager = GetComponent<UDPManager>();
        
    }

    public void EstablishConnection(string name, Color color)
    {
        tcpManager.EstablishConnection(name, color);
        udpManager.EstablishConnection();
    }
}
