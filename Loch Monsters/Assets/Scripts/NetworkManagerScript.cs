using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class NetworkManagerScript : MonoBehaviour
{
    //Make it a singleton
    public static NetworkManagerScript instance;

    //Server Connection Info
    const string serverIP = "127.0.0.1";
    const int port = 5555;

    //My Sockets
    TcpClient tcpClient;
    UdpClient udpClient; //TODO

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        EstablishConnection("AAAA");

    }


    public void EstablishConnection(string name)
    {
        ConnectMessage message = new ConnectMessage(new Color(.9f, .8f, .7f), name);

        Debug.Log("Can I even see output?");
        try
        {
            tcpClient = new TcpClient();
            tcpClient.Connect(serverIP, port);
            Debug.Log("Connected");

            NetworkStream tcpStream = tcpClient.GetStream();

            message.Send(tcpStream);
            Debug.Log("Data sent");
            EstablishUDPConnection();

            //URG, this shit still not working!
            //---read back the text---
            byte[] bytes = new byte[512];
            // Loop to receive all the data sent by the Server.
            var sb = new StringBuilder();
            
            var reply = tcpStream.Read(bytes, 0, bytes.Length);
            string converted = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
           
            
        }
        catch (Exception e)
        {
            Debug.Log("ERROR");
            Debug.Log(e.ToString());
        }


    }

    public void EstablishUDPConnection()
    {
        try
        {
            Debug.Log("Setting up UDP Client");
            udpClient = new UdpClient();
            udpClient.Connect(serverIP, port);
        }
        catch (Exception e)
        {

            Debug.Log("ERROR");
            Debug.Log(e.ToString());
        }
    }

    public void SendPosUpdate(Vector2 pos, Vector2 rotation)
    {
        string name = "AAAA";
        UTF8Encoding utfEncoding = new UTF8Encoding();
        //name = name.PadRight(32, ' ');
        List<byte> packet = new List<byte>();
        packet.AddRange(BitConverter.GetBytes('p'));
        packet.AddRange(BitConverter.GetBytes((short)name.Length));
        packet.AddRange(utfEncoding.GetBytes(name));
        packet.AddRange(BitConverter.GetBytes(pos.x));
        packet.AddRange(BitConverter.GetBytes(pos.y));
        packet.AddRange(BitConverter.GetBytes(rotation.x));
        packet.AddRange(BitConverter.GetBytes(rotation.y));

        //Should I be converting to big endian?

        udpClient = new UdpClient();
        udpClient.Connect(serverIP, port);
        udpClient.Send(packet.ToArray(), packet.Count);
    }
}
