using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

    //Adapted from: https://www.gamedev.net/forums/topic/433854-simple-c-udp-nonblocking/
    void ReceveUDP(IAsyncResult res)
    {
        IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);
        byte[] data = udpClient.EndReceive(res, ref remote);

        char type = BitConverter.ToChar(data, 0);

        switch (type)
        {
            case 'u':
                PositionUpdate p = new PositionUpdate(data);
                break;
            default:
                Debug.Log("Unknown message receved");
                break;
        }


        // get next packet
        udpClient.BeginReceive(ReceveUDP, null);

    }




    public void SendPosUpdate(Vector2 pos, Vector2 rotation)
    {
        PositionUpdate posUpdate = new PositionUpdate(id, pos, rotation);
        posUpdate.Send(udpClient);
    }

    


    /*
     * Sets up a connection to the server 
     * Creates both TCP and UDP clients
     * Sends connection message with name and color, receves back starting position 
     * Called when start button is pressed. Should be called only once 
     */
    public void EstablishConnection(string name)
    {
        playerName = name;
        ConnectMessage message = new ConnectMessage(new Color(.9f, .8f, .7f), name); //TODO: Send user entered color not placeholder

        try
        {
            //Make and connect the TCP client
            tcpClient = new TcpClient();
            tcpClient.Connect(serverIP, port);

            NetworkStream tcpStream = tcpClient.GetStream();

            //Sending the connection message 
            message.Send(tcpStream);

            //Get the type of message receved 
            byte[] type = new byte[2];

            tcpStream.Read(type, 0, type.Length);
            char typeChar = BitConverter.ToChar(type, 0);

            //Got connection response, process it
            if (typeChar == 'c')
            {
                //This could be put in a seprate function but it is only happening once and I don't think it would contrubute to readibility much
                byte[] num = new byte[4];
                tcpStream.Read(num, 0, num.Length);
                int idNum = BitConverter.ToInt32(num, 0);
                id = idNum;
                GameManager.instance.myID = idNum; //This is such a MESS, just shoot me!

                tcpStream.Read(num, 0, num.Length);
                int xPos = BitConverter.ToInt32(num, 0);


                tcpStream.Read(num, 0, num.Length);
                int yPos = BitConverter.ToInt32(num, 0);

                //This is bad but I don't know what else to do
                PlayerPrefs.SetInt("playerX", xPos);
                PlayerPrefs.SetInt("playerY", yPos);
            }
            else
            {
                throw new Exception("Message other then connection reply receved"); //TODO: Probley should be some way to recover from this
            }

            //Read in the next message
            tcpStream.Read(type, 0, type.Length);
            typeChar = BitConverter.ToChar(type, 0);

            //Check if this is the message containing data for all other monsters in the game
            if (typeChar == 'o')
            {
                //Get number of other snakes in the game
                byte[] num = new byte[2];
                tcpStream.Read(num, 0, num.Length);
                short numClients = BitConverter.ToInt16(num, 0);

                //Each other snake requires 60 bytes to store its data
                byte[] otherSnake = new byte[60];

                //Read in data for each of the other snakes listed
                for (int i = 0; i < numClients; i++)
                {
                    tcpStream.Read(otherSnake, 0, otherSnake.Length);
                    ProcessOtherSnake(otherSnake);
                }
            }


            //Initalize UDP client
            udpClient = new UdpClient();
            udpClient.Connect(serverIP, port);
            udpClient.BeginReceive(new AsyncCallback(ReceveUDP), null);

        }
        catch (Exception e)
        {
            Debug.Log("ERROR");
            Debug.Log(e.ToString());
        }
    }

    //Read in data for each other snake in the game and save it to the list manatained by the game manager
    void ProcessOtherSnake(byte [] otherSnake)
    {
        int index = 0;
        int snakeID = BitConverter.ToInt32(otherSnake, index);


        index += 4;
        byte[] nameBuffer = new List<Byte>(otherSnake).GetRange(index, 32).ToArray();

        string snakeName = System.Text.Encoding.UTF8.GetString(nameBuffer).Trim();
        index += 32;

        short snakeLength = BitConverter.ToInt16(otherSnake, index);
        index += 2;
        short colorR = BitConverter.ToInt16(otherSnake, index);
        index += 2;
        short colorG = BitConverter.ToInt16(otherSnake, index);
        index += 2;
        short colorB = BitConverter.ToInt16(otherSnake, index);
        index += 2;

        float xPos = BitConverter.ToSingle(otherSnake, index);
        index += 4;
        float yPos = BitConverter.ToSingle(otherSnake, index);
        index += 4;
        float xDir = BitConverter.ToSingle(otherSnake, index);
        index += 4;
        float yDir = BitConverter.ToSingle(otherSnake, index);

       
        Color snakeColor = new Color(((float)colorR / 255f), ((float)colorG / 255f), ((float)colorB / 255f));
        Vector2 pos = new Vector2(xPos, yPos);
        Vector2 dir = new Vector2(xPos, yPos);
        GameManager.instance.snakes.Add(snakeID, new Snake(snakeID, snakeName, snakeLength, snakeColor, pos, dir));
    }

}
