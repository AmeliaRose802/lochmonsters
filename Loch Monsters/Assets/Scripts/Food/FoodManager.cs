using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour, IMessageListener
{
    public GameObject foodPrefab;

    //Doing something like the object pool pattern so that the computer doens't get destroyed constantly creating and destroying food
    //It doens't instantate them all up front but instead makes them as needed so as to not waste memory
    Queue<GameObject> deadFoodObjects;
    Dictionary<int, GameObject> foodObjects;

    List<int> needConfirm;

    private void Awake()
    {
        foodObjects = new Dictionary<int, GameObject>();
        deadFoodObjects = new Queue<GameObject>();
        needConfirm = new List<int>();
    }

    private void Start()
    {
        MessageSystem.instance.Subscribe(MessageType.SPAWN_NEW_FOOD, this);
        MessageSystem.instance.Subscribe(MessageType.ATE_FOOD, this);
        MessageSystem.instance.Subscribe(MessageType.FOOD_EATEN, this);

        StartCoroutine(RetrySend());
    }

    public void Receive(IMessage message)
    {
        switch (message.GetMessageType())
        {
            case MessageType.SPAWN_NEW_FOOD:
                SpawnFood((SpawnFood)message);
                break;
            case MessageType.FOOD_EATEN:
                OtherAteFood eatenMessage = (OtherAteFood)message;
                FoodEaten(eatenMessage.foodID);
                break;
            case MessageType.ATE_FOOD:
                PlayerAteFood ateMessage = (PlayerAteFood)message;
                AteFood(ateMessage.foodID);
                break;
        }
    }

    private void SpawnFood(SpawnFood food)
    {
        GameObject newFood;
        if(deadFoodObjects.Count <= 0)
        {
            newFood = Instantiate(foodPrefab, transform);
        }
        else
        {
            newFood = deadFoodObjects.Dequeue();
            newFood.SetActive(true);
        }
        
        newFood.name = "Food " + food.id;
        foodObjects.Add(food.id, newFood);

        newFood.transform.position = food.pos;        
    }

    private void FoodEaten(int id)
    {
        if (foodObjects.ContainsKey(id))
        {
            foodObjects[id].SetActive(false);
            deadFoodObjects.Enqueue(foodObjects[id]);
            foodObjects.Remove(id);
        }
        if (needConfirm.Contains(id))
        {
            needConfirm.Remove(id);
        }
        
    }

    private void AteFood(int id)
    {

        if (foodObjects.ContainsKey(id))
        {
            foodObjects[id].SetActive(false);
            deadFoodObjects.Enqueue(foodObjects[id]);
            foodObjects.Remove(id);
            needConfirm.Add(id);

            //Firing this from here bipasses the double trigger enter bug by verifying that the list contains the object before adding a segment
            MessageSystem.instance.DispatchMessage(new AddPlayerSegment());
        }
    }

    private void OnDestroy()
    {
        MessageSystem.instance.Unsubscribe(MessageType.SPAWN_NEW_FOOD, this);
        MessageSystem.instance.Unsubscribe(MessageType.ATE_FOOD, this);
        MessageSystem.instance.Unsubscribe(MessageType.FOOD_EATEN, this);
        foodObjects.Clear();
    }

    IEnumerator RetrySend()
    {
        yield return new WaitForFixedUpdate();

        while (GameManager.instance.gameRunning)
        {
            yield return new WaitForSeconds(1);
            foreach (int id in needConfirm)
            {
                MessageSystem.instance.DispatchMessage(new PlayerAteFood(id, MessageType.RETRY_ATE_FOOD));
            }
        }
    }

}
