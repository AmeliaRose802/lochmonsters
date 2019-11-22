using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using UnityEngine;

public class TCPManager : MonoBehaviour
{
    const int tcpPort = 5555;
    const string serverIP = "127.0.0.1";

    TcpClient tcpClient;

    private void Start()
    {
        //Make and connect the TCP client
        tcpClient = new TcpClient();
    }
    /*
     * Sets up a connection to the server 
     * Sends connection message with name and color, receves back starting position 
     */
    public void EstablishConnection(string name)
    {
        ConnectMessage message = new ConnectMessage(new Color(.9f, .8f, .7f), name); //TODO: Send user entered color not placeholder
        StartMessage startMessage;
        try
        {
            tcpClient.Connect(serverIP, tcpPort);
            var tcpStream = tcpClient.GetStream();

            //Time latency 
            Stopwatch stopWatch = new Stopwatch();
            
            //Sending the connection message 
            var msg = message.GetMessage();

            stopWatch.Start();
            tcpStream.Write(msg, 0, msg.Length);

            //Get the type of message receved 
            byte[] type = new byte[2];

            tcpStream.Read(type, 0, type.Length);
            long latency = stopWatch.ElapsedMilliseconds;

            char typeChar = BitConverter.ToChar(type, 0);

            //Got connection response, process it
            if (typeChar == 'c')
            {
                //This could be put in a seprate function but it is only happening once and I don't think it would contrubute to readibility much
                byte[] connectResponse = new byte[20];

                tcpStream.Read(connectResponse, 0, connectResponse.Length);
                startMessage = new StartMessage(connectResponse, latency);
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
                    startMessage.otherSnakes.Add(new SnakeData(otherSnake));
                }
            }

            GameManager.instance.messageSystem.DispatchMessage(startMessage);

        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("ERROR");
            UnityEngine.Debug.Log(e.ToString());
        }
    }

}
