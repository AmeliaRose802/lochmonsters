using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour, IMessageListener
{
    public GameObject playerHead;
    public GameObject nonPlayerHead;
    public GameObject bodySegment;

    Dictionary<int, Transform> npHeads;
    Dictionary<int, long> lastUpdateTimes;

    [HideInInspector]
    public Transform playerHeadTranform;

    public static SnakeManager instance;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        npHeads = new Dictionary<int, Transform>();

        GameManager.instance.messageSystem.Subscribe(MessageType.SET_PLAYER_POSITION, this);
        GameManager.instance.messageSystem.Subscribe(MessageType.SPAWN_NON_PLAYER_SNAKE, this);
        GameManager.instance.messageSystem.Subscribe(MessageType.UPDATE_POSITION, this);
        lastUpdateTimes = new Dictionary<int, long>();
    }

    public void Receive(IMessage message)
    {
        
        switch (message.GetMessageType())
        {
            case MessageType.SET_PLAYER_POSITION:
                SetPlayerPos((SetPlayerPosition)message);
                break;
            case MessageType.SPAWN_NON_PLAYER_SNAKE:
                SpawnSnake(((SpawnSnake)message).snake);
                break;

            case MessageType.UPDATE_POSITION:
                UpdateSnakePosition((PositionUpdate)message);
                break;
        }
        
    }

    void SpawnSnake(SnakeData snake)
    {
        Debug.Log("Got message to spawn snake " + snake.name);

        GameObject container = Instantiate(new GameObject());
        container.name = "NonPlayerSnake";
        GameObject newHead = Instantiate(nonPlayerHead, container.transform);
        NPSnakeHead snakeHeadManager = newHead.GetComponent<NPSnakeHead>();

        //Set all nessary data for new snake
        newHead.transform.GetComponent<SpriteRenderer>().color = snake.color;
        newHead.transform.position = snake.pos;
        newHead.transform.up = snake.dir;
        
       
        //Spawn all body segments based on snake length
        for(int i = 0; i< snake.length - 1; i++)
        {
            GameObject newSegment = Instantiate(bodySegment, container.transform);
            newSegment.GetComponent<SpriteRenderer>().color = snake.color;
            snakeHeadManager.segments.Add(newSegment);
        }

        //Add the new snake created to the list of snakes
        npHeads.Add(snake.id, newHead.transform);
    }

    public void SetPlayerPos(SetPlayerPosition playerPosition)
    {
        
        GameObject container = Instantiate(new GameObject());
        container.name = "Player";
        var head = Instantiate(playerHead, container.transform);
        SnakeHead snakeHeadManager = head.GetComponent<SnakeHead>();

        head.transform.position = playerPosition.pos;
        head.transform.up = playerPosition.der;

        for (int i = 0; i < GameManager.DEFAULT_LENGTH -1; i++)
        {
            GameObject newSegment = Instantiate(bodySegment, container.transform);
            newSegment.GetComponent<SpriteRenderer>().color = GameManager.instance.playerColor;
            snakeHeadManager.segments.Add(newSegment);
        }

        playerHeadTranform = head.transform;
    }

    /*
     * Set the position for a non player snake. Does nothing if pos is not valid
     * */
    public void UpdateSnakePosition(PositionUpdate pos)
    {

        int id = pos.getID();

        if (ValidateNPID(id) && CheckUpToDate(id, pos.getTimestamp()))
        {

            Debug.Log("new pos = " + pos.getPos() + " " + pos.getDir());

            
            /*
            float elapsedTime = Math.Abs(GameManager.instance.gameTime - pos.getTimestamp());

            var expected = ClampToRange(pos.getPos() + (pos.getDir() * 4 * elapsedTime / 1000), new Vector2(25f, 25f));
            */

            Debug.Log("Current position"+npHeads[id].position+" New position: " + pos.getPos() + " diff " + (pos.getPos() - new Vector2(npHeads[id].position.x, npHeads[id].position.y)));


            lastUpdateTimes[id] = pos.getTimestamp();

            
            npHeads[id].position = pos.getPos();
            npHeads[id].up = pos.getDir();
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
            if (id == GameManager.instance.id)
            {
                throw new Exception("Got message about self");
            }
            if (!npHeads.ContainsKey(id))
            {
                throw new KeyNotFoundException("Unknown snake");
            }
            isOk = true;

        }
        catch (KeyNotFoundException e)
        {
            Debug.Log("Snake with ID: " + id);
            print("ERROR " + e.ToString());
        }
        catch (Exception e)
        {
            //print("ERROR " + e.ToString());
        }

        return isOk;
    }

    Vector2 ClampToRange(Vector2 val, Vector2 range)
    {
        Vector2 clamped = val;
        if (val.x < -range.x )
        {
            clamped.x = -range.x;
        }
        else if (val.x > range.x)
        {
            clamped.x = range.x;
        }

        if (val.y < -range.y)
        {
            clamped.y = -range.y;
        }
        else if (val.y > range.y)
        {
            clamped.y = range.y;
        }


        return clamped;
    }
}
