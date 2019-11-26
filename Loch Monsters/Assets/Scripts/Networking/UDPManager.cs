﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;


/*
 * Get and send UDP messages 
 * */
public class UDPManager: MonoBehaviour, IMessageListener
{
    const int udpPort = 5555;
    const string serverIP = "127.0.0.1";

    IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);

    UdpClient udpClient;

    private void Start()
    {
        GameManager.instance.messageSystem.Subscribe(MessageType.UPDATE_POSITION, this);
    }

    void OnApplicationQuit()
    {
        if (GameManager.instance.gameRunning)
        {
            udpClient.Close();
        }
        
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.gameRunning)
        {
            ReceveUDP();
        }
    }

    public void EstablishConnection()
    {
        udpClient = new UdpClient();
        udpClient.Connect(serverIP, udpPort);
        udpClient.Client.Blocking = false;
    }

    void ReceveUDP()
    {
        try
        {
            while (udpClient.Client.Available > 0)
            {
                var data = udpClient.Receive(ref remote);

                if (data.Length != 0)
                {
                    char type = BitConverter.ToChar(data, 0);

                    switch (type)
                    {
                        case 'u':
                            PositionUpdate p = new PositionUpdate(data);
                            GameManager.instance.messageSystem.DispatchMessage(p);
                            break;
                        case 'b':
                            Debug.Log("Got termination message");
                            TerminationMessage t = new TerminationMessage();
                            GameManager.instance.messageSystem.DispatchMessage(t);
                            break;
                        default:
                            Debug.Log("Unknown message receved");
                            break;
                    }
                }
            }
        }
        catch (SocketException e)
        {
            Debug.Log("Something went wrong with the UDP socket");
            TerminationMessage t = new TerminationMessage();
            GameManager.instance.messageSystem.DispatchMessage(t);
        }
        
    }


    public void SendUDPMessage(NetworkMessage message)
    {
        var m = message.GetMessage();
        udpClient.Send(message.GetMessage(), m.Length);
    }

    public void Receive(IMessage message)
    {
        switch (message.GetMessageType())
        {
            case MessageType.UPDATE_POSITION:
                SendUDPMessage((PositionUpdate)message);
            break;
        }
    }
}
