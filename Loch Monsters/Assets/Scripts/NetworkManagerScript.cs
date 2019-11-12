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

    int id;
    //Server Connection Info
    const string serverIP = "127.0.0.1";
    string playerName;
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
        DontDestroyOnLoad(this.gameObject);

    }


    public void EstablishConnection(string name)
    {
        playerName = name;
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
            byte[] type = new byte[2];
            
            var sb = new StringBuilder();
            
            var reply = tcpStream.Read(type, 0, type.Length);
            char typeChar = BitConverter.ToChar(type, 0);

            Debug.Log((int)typeChar);

            if(typeChar == 'c')
            {
                byte[] num = new byte[4];
                tcpStream.Read(num, 0, num.Length);
                Debug.Log("Got Connection Message");
                int idNum = BitConverter.ToInt32(num, 0);
                Debug.Log("Id " + idNum);
                id = idNum;

                tcpStream.Read(num, 0, num.Length);
                int xPos = BitConverter.ToInt32(num, 0);
                Debug.Log("X pos " + xPos);

                
                tcpStream.Read(num, 0, num.Length);
                int yPos = BitConverter.ToInt32(num, 0);
                Debug.Log("Y pos " + yPos);

                //This is bad but I don't know what else to do
                PlayerPrefs.SetInt("playerX", xPos);
                PlayerPrefs.SetInt("playerY", yPos);
            }
            
           // string converted = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
           // Debug.Log(converted);

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
        UTF8Encoding utfEncoding = new UTF8Encoding();
        //name = name.PadRight(32, ' ');
        List<byte> packet = new List<byte>();
        packet.AddRange(BitConverter.GetBytes('p'));
        packet.AddRange(BitConverter.GetBytes(id));
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
