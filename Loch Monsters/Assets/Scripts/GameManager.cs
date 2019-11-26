using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IMessageListener
{
    public const int DEFAULT_LENGTH = 3;
    public static GameManager instance;
    public string playerName;

    public NetworkManagerScript networkManager;
    public int id;
    public long gameTime;
    public long startTime;
    public bool gameRunning = false;
    public bool updateClock = false;

    public Color playerColor; //TODO: Let player choose
    
    public MessageSystem messageSystem;

    public SnakeManager snakeManager;


    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);


        networkManager = GetComponentInChildren<NetworkManagerScript>();
        messageSystem = GetComponent<MessageSystem>();
    }

    private void Start()
    {
        messageSystem.Subscribe(MessageType.START_GAME, this);
        messageSystem.Subscribe(MessageType.TERMINATION_MESSAGE, this);

    }
    private void FixedUpdate()
    {
        if (updateClock)
        {
            long epoch = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
            gameTime = epoch - startTime;
        }
    }

    public void Receive(IMessage message)
    {
        switch (message.GetMessageType())
        {
            case MessageType.START_GAME:
                print("Start game");
                StartCoroutine(StartGame((StartMessage)message));
                break;
            case MessageType.TERMINATION_MESSAGE:
                ExitGame();
                break;
        }
    }

    
    public void LaunchGame(string name)
    {
        networkManager.EstablishConnection(name, playerColor);
    }

    void ExitGame()
    {
        Debug.Log("Game no longer connected to the server");
        gameRunning = false;
        SceneManager.LoadScene("MenuScene");
    }

    IEnumerator StartGame(StartMessage startMessage)
    {
        id = startMessage.id;

        SceneManager.LoadScene("game");

        yield return new WaitForFixedUpdate(); //Wait a fixed update cycle to make sure that all new objects can init

        messageSystem.DispatchMessage(new SetPlayerPosition(startMessage.startPos, startMessage.startDir));

        foreach (var snake in startMessage.otherSnakes)
        {
            SpawnSnake spawnMessage = new SpawnSnake(snake);
            messageSystem.DispatchMessage(spawnMessage);
        }

        gameRunning = true; //Now start doing all the game processes since things are inited
    }


}
