using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeManager : MonoBehaviour, IMessageListener
{
    public GameObject playerHead;
    public GameObject nonPlayerHead;
    public GameObject bodySegment;
    public GameObject snakeKilledParticles;
    

    Dictionary<int, Transform> npTargets;
    Dictionary<int, long> lastUpdateTimes;

    Queue<GameObject> deadSnakeSegments;
    Queue<GameObject> deadNPSnakeHeads;
    Transform deadSnakeParent;
    Transform snakesContainer;

    [HideInInspector]
    Transform playerHeadTranform;

    void Awake()
    {

        npTargets = new Dictionary<int, Transform>();
        lastUpdateTimes = new Dictionary<int, long>();

        deadSnakeSegments = new Queue<GameObject>();
        deadNPSnakeHeads = new Queue<GameObject>();
        
    }

    void Start()
    {
        MessageSystem.instance.Subscribe(MessageType.CREATE_PLAYER, this);
        MessageSystem.instance.Subscribe(MessageType.SPAWN_NON_PLAYER_SNAKE, this);
        MessageSystem.instance.Subscribe(MessageType.UPDATE_NP_POSITION, this);
        MessageSystem.instance.Subscribe(MessageType.FOOD_EATEN, this);
        MessageSystem.instance.Subscribe(MessageType.ADD_PLAYER_SEGMENT, this);
        MessageSystem.instance.Subscribe(MessageType.KILL_SNAKE, this);
        deadSnakeParent = new GameObject().transform;
        deadSnakeParent.parent = gameObject.transform;
        deadSnakeParent.gameObject.name = "Dead Stuff";

        snakesContainer = new GameObject().transform;
        snakesContainer.parent = gameObject.transform;
        snakesContainer.gameObject.name = "Snakes";
    }

    void OnDestroy()
    {
        MessageSystem.instance.Unsubscribe(MessageType.CREATE_PLAYER, this);
        MessageSystem.instance.Unsubscribe(MessageType.SPAWN_NON_PLAYER_SNAKE, this);
        MessageSystem.instance.Unsubscribe(MessageType.UPDATE_NP_POSITION, this);
        MessageSystem.instance.Unsubscribe(MessageType.FOOD_EATEN, this);
        MessageSystem.instance.Unsubscribe(MessageType.ADD_PLAYER_SEGMENT, this);
        MessageSystem.instance.Unsubscribe(MessageType.KILL_SNAKE, this);
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
            case MessageType.KILL_SNAKE:
                RemoveSnake((KillSnake)message);
                break;

        }
        
    }

    void AddNonPlayerSnakeSegment(int id)
    {
        if (ValidateNPID(id))
        {
            GameObject npSnake = npTargets[id].parent.gameObject;

            NPSnakeHead snakeHeadScript = npSnake.GetComponent<NPSnakeHead>();

            GameObject newSegment = GetSegment(npSnake.transform.parent);

            var newColor = snakeHeadScript.snakeData.color;
            newColor.a = GlobalConsts.SEGMENT_TRANSPARINCY;
            newSegment.GetComponent<SpriteRenderer>().color = newColor;
            newSegment.GetComponent<TrailRenderer>().colorGradient = GetGradient(snakeHeadScript.snakeData.color);
            snakeHeadScript.segments.Add(newSegment);
            snakeHeadScript.Resize();
        }
    }

    void AddPlayerSegment()
    {
        SnakeHead headScript = playerHeadTranform.gameObject.GetComponent<SnakeHead>();

        var newSegment = GetSegment(playerHeadTranform.parent);
        newSegment.AddComponent(typeof(SegmentCollision));

        newSegment.transform.position = headScript.segments[headScript.segments.Count - 1].transform.position;
        newSegment.transform.rotation = headScript.segments[headScript.segments.Count - 1].transform.rotation;

        newSegment.GetComponent<TrailRenderer>().colorGradient = GetGradient(headScript.playerColor);

        var newColor = headScript.playerColor;
        newColor.a = GlobalConsts.SEGMENT_TRANSPARINCY;
        newSegment.GetComponent<SpriteRenderer>().color = newColor;

        
        headScript.segments.Add(newSegment);
        headScript.Resize();
    }

    void SpawnSnake(SnakeData snake)
    {
        GameObject snakeContainer = GetNPSnake(snake.name, snake.id);

        GameObject newHead = snakeContainer.transform.GetChild(0).gameObject;

        //Non player snakes follow their targets for lerping reasons
        Transform target = newHead.transform.GetChild(0);

        NPSnakeHead snakeHeadManager = newHead.GetComponent<NPSnakeHead>();

        newHead.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text = snake.name;

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
            GameObject newSegment = GetSegment(snakeContainer.transform);

            var newColor = snake.color;
            newColor.a = GlobalConsts.SEGMENT_TRANSPARINCY;
            newSegment.GetComponent<SpriteRenderer>().color = newColor;
            newSegment.GetComponent<TrailRenderer>().colorGradient = GetGradient(snake.color);
            snakeHeadManager.segments.Add(newSegment);
        }

        snakeHeadManager.Resize();
        //Add the new snake created to the list of snakes
        npTargets.Add(snake.id, target);
    }

    void RemoveSnake(KillSnake message)
    {
        if(message.id == GameManager.instance.id)
        {
            GameManager.instance.gameRunning = false;
            MessageSystem.instance.DispatchMessage(new EndGame(EndType.END_LOSE, MessageType.END_GAME, ""));

        }
        else if (ValidateNPID(message.id))
        {
            var segments = npTargets[message.id].parent.GetComponent<NPSnakeHead>().segments;

            foreach(var segment in segments)
            {
                segment.gameObject.SetActive(false);
                segment.transform.parent = deadSnakeParent.transform;
                deadSnakeSegments.Enqueue(segment);
            }

            segments.Clear();

            var deadHead = npTargets[message.id].parent.parent.gameObject;
            var particles = Instantiate(snakeKilledParticles,deadHead.transform.GetChild(0).transform.position, deadHead.transform.GetChild(0).transform.rotation);
            //Destroy(particles, 2);

            deadHead.SetActive(false);
            deadHead.transform.parent = deadSnakeParent.transform;
            deadNPSnakeHeads.Enqueue(deadHead);

            npTargets.Remove(message.id);
            lastUpdateTimes.Remove(message.id);
        }
    }

    void SpawnPlayer(CreatePlayer createPlayer)
    { 
        GameObject container = Instantiate(new GameObject());
        container.name = "Player";
        container.transform.parent = snakesContainer;
        var head = Instantiate(playerHead, container.transform);
        head.name = "PlayerHead";
        SnakeHead snakeHeadManager = head.GetComponent<SnakeHead>();
        snakeHeadManager.playerColor = createPlayer.color;
        snakeHeadManager.name = createPlayer.name;

        head.GetComponent<TrailRenderer>().colorGradient = GetGradient(snakeHeadManager.playerColor);
        head.GetComponent<SpriteRenderer>().color = snakeHeadManager.playerColor;



        head.transform.position = createPlayer.pos;
        head.transform.up = createPlayer.der;

        for (int i = 0; i < GlobalConsts.DEFAULT_LENGTH -1; i++)
        {
            GameObject newSegment = Instantiate(bodySegment, container.transform);
            newSegment.AddComponent(typeof(SegmentCollision));
            newSegment.GetComponent<TrailRenderer>().colorGradient = GetGradient(snakeHeadManager.playerColor);
            var newColor = snakeHeadManager.playerColor;
            newColor.a = GlobalConsts.SEGMENT_TRANSPARINCY;
            newSegment.GetComponent<SpriteRenderer>().color = newColor;
            snakeHeadManager.segments.Add(newSegment);
        }

        playerHeadTranform = head.transform;
    }

    GameObject GetSegment(Transform parent)
    {
        GameObject newSegment;
        if (deadSnakeSegments.Count <= 0)
        {
            newSegment = Instantiate(bodySegment, parent);
        }
        else
        {
            newSegment = deadSnakeSegments.Dequeue();
            newSegment.transform.parent = parent;
            newSegment.SetActive(true);
        }

        return newSegment;
    }

    GameObject GetNPSnake(string name, int id)
    {
        GameObject container;
        if (deadNPSnakeHeads.Count <= 0)
        {
            container = Instantiate(new GameObject());
            container.transform.parent = snakesContainer;
            Instantiate(nonPlayerHead, container.transform);
        }
        else
        {
            container = deadNPSnakeHeads.Dequeue();
            container.SetActive(true);
            
            container.transform.parent = snakesContainer;
        }

        container.name = id + " : "+name;
        container.tag = "otherPlayer";

        return container;
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
            print("ERROR " + e.ToString());
        }
        catch (Exception e)
        {
            //print("ERROR " + e.ToString());
        }

        return isOk;
    }

    Gradient GetGradient(Color color)
    {
        Gradient gradient = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        GradientColorKey[] colorKey = new GradientColorKey[1];
        colorKey[0].color = color;
        colorKey[0].time = 0.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[1];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;

        gradient.SetKeys(colorKey, alphaKey);

        return gradient;
    }
}
