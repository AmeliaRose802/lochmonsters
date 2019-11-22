using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour, IMessageListener
{
    public GameObject player;
    public GameObject nonPlayerSnakePrefab;
    public GameObject nonPlayerBodySegment;

    Dictionary<int, GameObject> npSnakes;
    Dictionary<int, long> lastUpdateTimes;

    public void Awake()
    {
        npSnakes = new Dictionary<int, GameObject>();

        GameManager.instance.messageSystem.Subscribe(MessageType.SET_PLAYER_POSITION, this);
        GameManager.instance.messageSystem.Subscribe(MessageType.SPAWN_NON_PLAYER_SNAKE, this);
        GameManager.instance.messageSystem.Subscribe(MessageType.UPDATE_POSITION, this);
        lastUpdateTimes = new Dictionary<int, long>();
    }

    public void Receive(IMessage message)
    {
        Debug.Log(message.GetMessageType());
        
        switch (message.GetMessageType())
        {
            case MessageType.SET_PLAYER_POSITION:
                SetPlayerPos((SetPlayerPosition)message);
                break;
            case MessageType.SPAWN_NON_PLAYER_SNAKE:
                print("Spawn a non player snake");
                SpawnSnake(((SpawnSnake)message).snake);
                break;

            case MessageType.UPDATE_POSITION:
                UpdateSnakePosition((PositionUpdate)message);
                break;
        }
        
    }

    void SpawnSnake(SnakeData snake)
    {
        GameObject newSnake = Instantiate(nonPlayerSnakePrefab);
        NPSnakeHead snakeHeadManager = newSnake.GetComponentInChildren<NPSnakeHead>();

        //Set all nessary data for new snake
        newSnake.transform.GetChild(0).GetComponent<SpriteRenderer>().color = snake.color;
        newSnake.transform.position = snake.pos;
        newSnake.transform.right = snake.dir;
        
       
        //Spawn all body segments based on snake length
        for(int i = 0; i< snake.length; i++)
        {
            GameObject newSegment = Instantiate(nonPlayerBodySegment, newSnake.transform);
            newSegment.GetComponent<SpriteRenderer>().color = snake.color;
            snakeHeadManager.segments.Add(newSegment);
        }

        //Add the new snake created to the list of snakes
        npSnakes.Add(snake.id, newSnake);
    }

    public void SetPlayerPos(SetPlayerPosition playerPosition)
    {
        player.transform.position = playerPosition.pos;
        player.transform.right = playerPosition.der;
    }

    /*
     * Set the position for a non player snake. Does nothing if pos is not valid
     * */
    public void UpdateSnakePosition(PositionUpdate pos)
    {
        int id = pos.getID();

        if (ValidateNPID(id) && CheckUpToDate(id, pos.getTimestamp()))
        {
            lastUpdateTimes[id] = pos.getTimestamp();

            npSnakes[id].transform.position = pos.getPos();
            npSnakes[id].transform.right = pos.getDir();
        }
    }


    /*
     * Returns true if this packet is up to date or if no data exists for it already
     * */
    bool CheckUpToDate(int id, long timestamp)
    {
        return !(lastUpdateTimes.ContainsKey(id) && lastUpdateTimes[id] > timestamp);
    }

    /*
     * Returns true if the number is a valid ID for a non player snake
     * */
    bool ValidateNPID(int id)
    {
        bool isOk = false;
        try
        {
            if (id != GameManager.instance.id)
            {
                throw new Exception("Got message about self");
            }
            if (!npSnakes.ContainsKey(id))
            {
                throw new KeyNotFoundException("Unknown snake");
            }
            isOk = true;

        }
        catch (KeyNotFoundException e)
        {
            print("ERROR " + e.ToString());
        }
        catch (Exception e)
        {
            print("ERROR " + e.ToString());
        }

        return isOk;
    }

    void SetSnakeColor(GameObject snake, Color color)
    {
        foreach (Transform child in snake.transform)
        {
            child.gameObject.GetComponent<SpriteRenderer>().color = color;
        }
    }
}
