using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using UnityEngine;

public class TCPManager : MonoBehaviour, IMessageListener
{
    Stopwatch stopWatch;
    TcpClient tcpClient;

    TimeMessage activeTimeMessage;

    private void Start()
    {
        MessageSystem.instance.Subscribe(MessageType.ATE_FOOD, this);
        MessageSystem.instance.Subscribe(MessageType.RETRY_ATE_FOOD, this);
        MessageSystem.instance.Subscribe(MessageType.HIT_SNAKE, this);
        MessageSystem.instance.Subscribe(MessageType.HIT_BY, this);
        MessageSystem.instance.Subscribe(MessageType.END_GAME, this);
        MessageSystem.instance.Subscribe(MessageType.CONNECT_GAME, this);
        MessageSystem.instance.Subscribe(MessageType.GAME_RUNNING, this);
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.gameRunning)
        {
            ReceveTCP();
        }
    }

    //Get tcp messages from the network
    private void ReceveTCP()
    {
        try
        {
            if (tcpClient.Available > 0)
            {
                byte[] message = new byte[512];

                var data = tcpClient.GetStream().Read(message, 0, message.Length);

                if (data != 0)
                {
                    char type = BitConverter.ToChar(message, 0);

                    //Trim off the part of the buffer containing the type so it is ready to be converted
                    var messageBody = new List<byte>(message);
                    message = messageBody.GetRange(2, messageBody.Count - 2).ToArray();

                    switch (type)
                    {
                        case 'n':
                            MessageSystem.instance.DispatchMessage(new SpawnNPSnake(message));
                            break;
                        case 'f':
                            MessageSystem.instance.DispatchMessage(new SpawnFood(message));
                            break;
                        case 'a':
                            MessageSystem.instance.DispatchMessage(new OtherAteFood(message));
                            break;
                        case 'l':
                            MessageSystem.instance.DispatchMessage(new KillSnake(message));
                            break;
                        case 't':
                            activeTimeMessage.SetGameTime(message);
                            break;
                        default:
                            print("Unknown packet receved");
                            break;
                    }
                }
            }
        }
        catch (Exception e)
        {
            MessageSystem.instance.DispatchMessage(new EndGame(EndType.END_SERVER_ERROR, MessageType.END_GAME, "Something went wrong sending TCP: " + e.ToString().Split(':')[1]));
        }



    }


    //Get messages from the message system
    public void Receive(IMessage message)
    {
        switch (message.GetMessageType())
        {
            case MessageType.ATE_FOOD:
                SendTCP((INetworkMessage)message);
                break;
            case MessageType.RETRY_ATE_FOOD:
                SendTCP((INetworkMessage)message);
                break;
            case MessageType.END_GAME:
                TerminateConnection();
                break;
            case MessageType.CONNECT_GAME:
                EstablishConnection((LaunchGame)message);
                break;
            case MessageType.GAME_RUNNING:
                StartCoroutine(SyncClock());
                break;
            case MessageType.HIT_SNAKE:
                SendTCP((INetworkMessage)message);
                break;
            case MessageType.HIT_BY:
                print("Got hit by message");
                SendTCP((INetworkMessage)message);
                break;
            default:
                print("Got something else");
                break;
        }
    }

    private void SendTCP(INetworkMessage message)
    {
        try
        {
            var m = message.GetMessage();

            tcpClient.GetStream().Write(message.GetMessage(), 0, m.Length);
        }
        catch (ObjectDisposedException e)
        {
            print(e);
            MessageSystem.instance.DispatchMessage(new EndGame(EndType.END_SERVER_ERROR, MessageType.END_GAME, "Something went wrong sending TCP: " + e.ToString().Split(':')[1]));
        }
        catch (SocketException e)
        {
            print(e);
            MessageSystem.instance.DispatchMessage(new EndGame(EndType.END_SERVER_ERROR, MessageType.END_GAME, "Something went wrong sending TCP: " + e.ToString().Split(':')[1]));
        }
        catch (Exception e)
        {
            print(e);
        }

    }

    private void OnDestroy()
    {
        TerminateConnection();
        MessageSystem.instance.Unsubscribe(MessageType.ATE_FOOD, this);
        MessageSystem.instance.Unsubscribe(MessageType.END_GAME, this);
        MessageSystem.instance.Unsubscribe(MessageType.CONNECT_GAME, this);
    }

    private void OnApplicationQuit()
    {
        TerminateConnection();
    }

    //Close the connection with the server (politely if possible)
    private void TerminateConnection()
    {
        //This could fail becouse the server has terminatednbut the tcpClient still needs to be closed
        try
        {
            tcpClient.GetStream().Write(BitConverter.GetBytes('e'), 0, 2);
            print("Sending terminate connection message on TCP");

        }
        catch (Exception) { }

        try
        {
            tcpClient.Close();
        }
        catch (Exception) { }
    }

    //Sends connection message with name and color, receves back starting position 
    private void EstablishConnection(LaunchGame launchMessage)
    {
        //Make and connect the TCP client
        tcpClient = new TcpClient();
        stopWatch = new Stopwatch();
        byte[] type = new byte[2];

        ConnectMessage connectMessage = new ConnectMessage(launchMessage.playerColor, launchMessage.playerName); //TODO: Send user entered color not placeholder
        var msgBytes = connectMessage.GetMessage();

        StartGame startMessage;

        try
        {
            tcpClient.Connect(launchMessage.serverIP, launchMessage.serverPort);
            var tcpStream = tcpClient.GetStream();

            stopWatch.Start();

            //Sending the connection message 
            tcpStream.Write(msgBytes, 0, msgBytes.Length);

            //Read the reply
            tcpStream.Read(type, 0, type.Length);

            //Get latency
            long latency = stopWatch.ElapsedMilliseconds;
            stopWatch.Reset();

            //Get type
            char typeChar = BitConverter.ToChar(type, 0);

            //Got connection response, process it
            if (typeChar == 'c')
            {
                byte[] connectResponse = new byte[20];
                byte[] shortNum = new byte[2];
                byte[] otherSnake = new byte[60];
                byte[] food = new byte[12];

                tcpStream.Read(connectResponse, 0, connectResponse.Length);

                //Parse the data recved into a new start message
                startMessage = new StartGame(launchMessage.playerName, launchMessage.playerColor, connectResponse, latency);

                //Get number of other clients

                tcpStream.Read(shortNum, 0, shortNum.Length);

                short numClients = BitConverter.ToInt16(shortNum, 0);

                //Read in data for each of the other snakes listed
                for (int i = 0; i < numClients; i++)
                {
                    tcpStream.Read(otherSnake, 0, otherSnake.Length);
                    startMessage.otherSnakes.Add(new SnakeData(otherSnake));
                }

                //Get amount of food in game
                tcpStream.Read(shortNum, 0, shortNum.Length);
                short numFood = BitConverter.ToInt16(shortNum, 0);

                for (int i = 0; i < numFood; i++)
                {
                    tcpStream.Read(food, 0, food.Length);
                    startMessage.foodInGame.Add(new FoodData(food));
                }

                MessageSystem.instance.DispatchMessage(startMessage);

                //Now the syncronus set up is done put the client into non blocking mode
                tcpClient.Client.Blocking = false;
            }
            else
            {
                throw new Exception("Message other then connection reply receved"); //TODO: Probley should be some way to recover from this

            }

        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("ERROR");
            UnityEngine.Debug.Log(e.ToString());
            MessageSystem.instance.DispatchMessage(new ConnectFailed(e.ToString()));
        }
    }


    //TODO: Put this in its own class
    IEnumerator SyncClock()
    {
        while (GameManager.instance.gameRunning)
        {
            activeTimeMessage = new TimeMessage();
            SendTCP(activeTimeMessage);
            yield return new WaitForSeconds(1);
        }
    }
}
