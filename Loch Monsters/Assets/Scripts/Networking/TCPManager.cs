using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using UnityEngine;

public class TCPManager : MonoBehaviour, IMessageListener
{
    const float CLOCK_SYNC_INTERVAL = 5;
    const int tcpPort = 5555;
    const string serverIP = "127.0.0.1";
    long clockSyncSendTime = 0;
    Stopwatch stopWatch;

    TcpClient tcpClient;

    private void Start()
    {
        //Make and connect the TCP client
        tcpClient = new TcpClient();
        stopWatch = new Stopwatch();


        GameManager.instance.messageSystem.Subscribe(MessageType.ATE_FOOD, this);
    }

    void OnApplicationQuit()
    {
        try
        {
            tcpClient.GetStream().Write(BitConverter.GetBytes('e'), 0, 2);
            tcpClient.Close();
        }
        catch (Exception) { }
    }


    private void FixedUpdate()
    {
        if (GameManager.instance.gameRunning)
        {
            ReceveTCP();
        }
    }


    void ReceveTCP()
    {
        if (tcpClient.Available > 0)
        {
            byte [] buffer = new byte[512];

            var data = tcpClient.GetStream().Read(buffer, 0, buffer.Length);

            if (data != 0)
            {
                char type = BitConverter.ToChar(buffer, 0);

                //Trim off the part of the buffer containing the type so it is ready to be converted
                var test = new List<Byte>(buffer);
                buffer = test.GetRange(2, test.Count - 2).ToArray();

                switch (type)
                {
                    case 'n':
                        SpawnSnake message = new SpawnSnake(buffer);
                        GameManager.instance.messageSystem.DispatchMessage(message);
                        break;
                    case 't':
                        HandleClockSyncReply(buffer);
                        break;
                    case 'f':
                        FoodUpdate foodUpdate = new FoodUpdate(buffer);
                        GameManager.instance.messageSystem.DispatchMessage(foodUpdate);
                        break;
                    default:
                        print("Unknown packet receved");
                        break;
                }
            }
        }
    }


    /*
     * Sets up a connection to the server 
     * Sends connection message with name and color, receves back starting position 
     */
    public void EstablishConnection(string name, Color color)
    {
        ConnectMessage message = new ConnectMessage(color, name); //TODO: Send user entered color not placeholder
        StartMessage startMessage;
        try
        {
            tcpClient.Connect(serverIP, tcpPort);
            var tcpStream = tcpClient.GetStream();

            //Time latency 
            
            
            //Sending the connection message 
            var msg = message.GetMessage();

            stopWatch.Start();
            tcpStream.Write(msg, 0, msg.Length);

            //Get the type of message receved 
            byte[] type = new byte[2];

            tcpStream.Read(type, 0, type.Length);
            long latency = stopWatch.ElapsedMilliseconds;
            stopWatch.Reset();

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
                print("num clients " + numClients);

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

            //Now the syncronus set up is done put the client into non blocking mode
            tcpClient.Client.Blocking = false;

        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("ERROR");
            UnityEngine.Debug.Log(e.ToString());
        }
    }

    public void Receive(IMessage message)
    {
        switch (message.GetMessageType())
        {
            case MessageType.REQUEST_CLOCK_SYNC:
                SyncClock();
                break;
            case MessageType.ATE_FOOD:
                SendTCPMessage((NetworkMessage)message);
                break;
            default:
                print("Got something else");
                break;
        }
    }

    IEnumerator SyncClock()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(CLOCK_SYNC_INTERVAL);

            if (GameManager.instance.gameRunning)
            {
                print("Time to send another clock sync message. Current time: "+GameManager.instance.gameTime);
                ClockSyncMessage syncClock = new ClockSyncMessage();
                tcpClient.GetStream().Write(syncClock.GetMessage(), 0, syncClock.GetMessage().Length);
                stopWatch.Start(); //Start timing latency

            }
        }
        
    }

    public void SendTCPMessage(NetworkMessage message)
    {
        var m = message.GetMessage();
        tcpClient.GetStream().Write(message.GetMessage(),0, m.Length);
    }

    void HandleClockSyncReply(byte [] reply)
    {
        var clockSync = new ClockSyncMessage(reply, stopWatch.ElapsedMilliseconds);
        clockSync.SetClock();
        stopWatch.Reset();
    }
}
