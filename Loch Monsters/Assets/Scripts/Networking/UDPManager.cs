using System;
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

    private void Update()
    {
        if (GameManager.instance.gameRunning)
        {
            //ReceveUDP();
        }
    }

    public void EstablishConnection()
    {
        udpClient = new UdpClient();
        udpClient.Connect(serverIP, udpPort);
    }

    void ReceveUDP()
    {
        //byte[] data = udpClient.EndReceive(res, ref remote);
        udpClient.Client.Blocking = false;

        if (udpClient.Client.Available > 0)
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
                    default:
                        Debug.Log("Unknown message receved");
                        break;
                }
            }
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
                Debug.Log("Got position update message from message system");
                SendUDPMessage((PositionUpdate)message);
            break;
        }
    }
}
