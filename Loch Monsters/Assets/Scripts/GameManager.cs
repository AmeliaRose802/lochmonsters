using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IMessageListener
{
    public static GameManager instance;
    public string playerName;

    public NetworkManagerScript networkManager;
    public int id;
    public long gameTime;
    public bool gameRunning = false;
    public bool updateClock = false;
    
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
    }
    private void Update()
    {
        if (updateClock)
        {
            gameTime += (long)(Time.deltaTime * 1000);
        }
    }

    public void Receive(IMessage message)
    {
        if(message.GetMessageType() == MessageType.START_GAME)
        {
            print("Start game");
            StartCoroutine(StartGame((StartMessage)message));
        }
    }

    public void LaunchGame(string name)
    {
        networkManager.EstablishConnection(name);
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
    }


}
