using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class UDPManager: MonoBehaviour, IMessageListener
{
    IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);

    UdpClient udpClient;

    private void Start()
    {
        MessageSystem.instance.Subscribe(MessageType.UPDATE_NP_POSITION, this);
        MessageSystem.instance.Subscribe(MessageType.END_GAME, this);
        MessageSystem.instance.Subscribe(MessageType.CONNECT_GAME, this);
        MessageSystem.instance.Subscribe(MessageType.SEND_PLAYER_POSITION, this);
    }

    private void OnDestroy()
    {
        MessageSystem.instance.Unsubscribe(MessageType.UPDATE_NP_POSITION, this);
        MessageSystem.instance.Unsubscribe(MessageType.END_GAME, this);
        MessageSystem.instance.Unsubscribe(MessageType.CONNECT_GAME, this);
        MessageSystem.instance.Unsubscribe(MessageType.SEND_PLAYER_POSITION, this);
    }

    void OnApplicationQuit()
    {
        TerminateConnection();
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.gameRunning)
        {
            ReceveUDP();
        }
    }

    private void ReceveUDP()
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
                            MessageSystem.instance.DispatchMessage(new PositionUpdate(data));
                            break;
                        case 'b':
                            MessageSystem.instance.DispatchMessage(new EndGame());
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
            Debug.Log("Something went wrong with the UDP socket "+ e);
            MessageSystem.instance.DispatchMessage(new EndGame());
        }
        
    }

    private void SendUDPMessage(INetworkMessage message)
    {
        try
        {
            var m = message.GetMessage();
            udpClient.Send(m, m.Length);
        }
        catch (Exception e)
        {
            Debug.Log("Something went wrong sending UDP: " + e);
        }

    }

    public void Receive(IMessage message)
    {
        switch (message.GetMessageType())
        {
            case MessageType.SEND_PLAYER_POSITION:
                SendUDPMessage((PositionUpdate)message);
                break;
            case MessageType.END_GAME:
                TerminateConnection();
                break;
            case MessageType.CONNECT_GAME:
                EstablishConnection((LaunchGame)message);
                break;
        }
    }

    private void EstablishConnection(LaunchGame message)
    {
        udpClient = new UdpClient();
        udpClient.Connect(message.serverIP, message.serverPort);
        udpClient.Client.Blocking = false;
    }

    private void TerminateConnection()
    {
        try
        {
            udpClient.Close();
        }
        catch (Exception) { };
    }
}
