using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IMessageListener
{
    //Static
    public static GameManager instance;

    public int id;    

    public long gameTime;
    public long startTime;
    public bool gameRunning = false;
    public bool updateClock = false;

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
    }

    private void Start()
    {
        MessageSystem.instance.Subscribe(MessageType.START_GAME, this);
        MessageSystem.instance.Subscribe(MessageType.END_GAME, this);
    }

    public void Receive(IMessage message)
    {
        switch (message.GetMessageType())
        {
            case MessageType.START_GAME:
                StartCoroutine(StartGame((StartGame)message));
                break;
            case MessageType.END_GAME:
                ExitGame();
                break;
        }
    }

    private void FixedUpdate()
    {
        if (updateClock)
        {
            long epoch = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
            gameTime = epoch - startTime;
        }
    }

    void ExitGame()
    {
        Debug.Log("Game no longer connected to the server");
        gameRunning = false;
        SceneManager.LoadScene("MenuScene");
    }

    IEnumerator StartGame(StartGame startMessage)
    {
        id = startMessage.id;

        SceneManager.LoadScene("game");

        yield return new WaitForFixedUpdate(); //Wait a fixed update cycle to make sure that all new objects can init

        MessageSystem.instance.DispatchMessage(new CreatePlayer(startMessage.playerName, startMessage.playerColor, startMessage.startPos, startMessage.startDir));

        foreach (var snake in startMessage.otherSnakes)
        {
            SpawnNPSnake spawnMessage = new SpawnNPSnake(snake);
            MessageSystem.instance.DispatchMessage(spawnMessage);
        }

        foreach (var food in startMessage.foodInGame)
        {
            SpawnFood spawnMessage = new SpawnFood(food);
            MessageSystem.instance.DispatchMessage(spawnMessage);
        }


        gameRunning = true; //Now start doing all the game processes since things are inited
    }
}
