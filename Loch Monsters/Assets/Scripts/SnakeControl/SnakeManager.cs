using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour, IMessageListener
{
    public GameObject playerHead;
    public GameObject nonPlayerHead;
    public GameObject bodySegment;

    Dictionary<int, Transform> npTargets;
    Dictionary<int, long> lastUpdateTimes;

    [HideInInspector]
    Transform playerHeadTranform;

    void Awake()
    {

        npTargets = new Dictionary<int, Transform>();

        MessageSystem.instance.Subscribe(MessageType.CREATE_PLAYER, this);
        MessageSystem.instance.Subscribe(MessageType.SPAWN_NON_PLAYER_SNAKE, this);
        MessageSystem.instance.Subscribe(MessageType.UPDATE_NP_POSITION, this);
        MessageSystem.instance.Subscribe(MessageType.FOOD_EATEN, this);
        MessageSystem.instance.Subscribe(MessageType.ADD_PLAYER_SEGMENT, this);
        lastUpdateTimes = new Dictionary<int, long>();
    }

    void OnDestroy()
    {
        MessageSystem.instance.Unsubscribe(MessageType.CREATE_PLAYER, this);
        MessageSystem.instance.Unsubscribe(MessageType.SPAWN_NON_PLAYER_SNAKE, this);
        MessageSystem.instance.Unsubscribe(MessageType.UPDATE_NP_POSITION, this);
        MessageSystem.instance.Unsubscribe(MessageType.FOOD_EATEN, this);
        MessageSystem.instance.Unsubscribe(MessageType.ADD_PLAYER_SEGMENT, this);
        npTargets.Clear();
        lastUpdateTimes.Clear();
    }

    public void Receive(IMessage message)
    {
        switch (message.GetMessageType())
        {
            case MessageType.CREATE_PLAYER:
                SpawnPlayer((CreatePlayer)message);
                break;
            case MessageType.SPAWN_NON_PLAYER_SNAKE:
                SpawnSnake(((SpawnNPSnake)message).snake);
                break;

            case MessageType.UPDATE_NP_POSITION:
                UpdateNPSnakePosition((PositionUpdate)message);
                break;
            case MessageType.FOOD_EATEN:
                AddNonPlayerSnakeSegment(((OtherAteFood)message).snakeID);
                break;
            case MessageType.ADD_PLAYER_SEGMENT:
                AddPlayerSegment();
                break;

        }
        
    }

    void AddNonPlayerSnakeSegment(int id)
    {
        if (ValidateNPID(id))
        {
            GameObject npSnake = npTargets[id].parent.gameObject;

            NPSnakeHead snakeHeadScript = npSnake.GetComponent<NPSnakeHead>();

            GameObject newSegment = Instantiate(bodySegment, npSnake.transform.parent);
            newSegment.GetComponent<SpriteRenderer>().color = snakeHeadScript.snakeData.color;

            snakeHeadScript.segments.Add(newSegment);
        }
    }

    void AddPlayerSegment()
    {

        SnakeHead headScript = playerHeadTranform.gameObject.GetComponent<SnakeHead>();

        var newSegment = Instantiate(bodySegment, playerHeadTranform.parent);
        newSegment.transform.position = headScript.segments[headScript.segments.Count - 1].transform.position;
        newSegment.transform.rotation = headScript.segments[headScript.segments.Count - 1].transform.rotation;
        newSegment.GetComponent<SpriteRenderer>().color = headScript.playerColor;

        headScript.segments.Add(newSegment);

    }

    void SpawnSnake(SnakeData snake)
    {
        Debug.Log("Got message to spawn snake " + snake.name);

        GameObject container = Instantiate(new GameObject());
        container.name = "NP Snake "+snake.name;
        GameObject newHead = Instantiate(nonPlayerHead, container.transform);

        //Non player snakes follow their targets for lerping reasons
        Transform target = newHead.transform.GetChild(0);

        NPSnakeHead snakeHeadManager = newHead.GetComponent<NPSnakeHead>();

        snakeHeadManager.snakeData = snake;
        //Set all nessary data for new snake
        newHead.transform.GetComponent<SpriteRenderer>().color = snake.color;
        newHead.transform.position = snake.pos;
        //newHead.transform.up = snake.dir;
        target.position = snake.pos;
        target.transform.up = snake.dir;

       
        //Spawn all body segments based on snake length
        for(int i = 0; i< snake.length - 1; i++)
        {
            GameObject newSegment = Instantiate(bodySegment, container.transform);
            newSegment.GetComponent<SpriteRenderer>().color = snake.color;
            snakeHeadManager.segments.Add(newSegment);
        }

        //Add the new snake created to the list of snakes
        npTargets.Add(snake.id, target);
    }

    void SpawnPlayer(CreatePlayer createPlayer)
    {
        
        GameObject container = Instantiate(new GameObject());
        container.name = "Player";
        var head = Instantiate(playerHead, container.transform);
        head.name = "PlayerHead";
        SnakeHead snakeHeadManager = head.GetComponent<SnakeHead>();
        snakeHeadManager.playerColor = createPlayer.color;
        snakeHeadManager.name = createPlayer.name;

        head.transform.position = createPlayer.pos;
        head.transform.up = createPlayer.der;

        for (int i = 0; i < GlobalConsts.DEFAULT_LENGTH -1; i++)
        {
            GameObject newSegment = Instantiate(bodySegment, container.transform);
            newSegment.GetComponent<SpriteRenderer>().color = snakeHeadManager.playerColor;
            snakeHeadManager.segments.Add(newSegment);
        }

        playerHeadTranform = head.transform;
    }

    /*
     * Set the position for a non player snake. Does nothing if pos is not valid
     * */
    void UpdateNPSnakePosition(PositionUpdate pos)
    {
        
        int id = pos.id;

        if (ValidateNPID(id) && CheckUpToDate(id, pos.timestamp))
        {
            
            float elapsedTime = Math.Abs(GameManager.instance.gameTime - pos.timestamp);

            var expected = GlobalConsts.ClampToRange(pos.position + (pos.direction * 4 * elapsedTime / 1000), new Vector2(25f, 25f));

            lastUpdateTimes[id] = pos.timestamp;

            npTargets[id].position = expected;
            npTargets[id].up = pos.direction;
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
            if (!npTargets.ContainsKey(id))
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
}
