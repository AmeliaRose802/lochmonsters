using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour, IMessageListener
{
    public GameObject foodPrefab;

    [HideInInspector]
    public Dictionary<int, GameObject> foodObjects;

    public static FoodManager instance;

    private void Awake()
    {
        foodObjects = new Dictionary<int, GameObject>();
        instance = this;
    }

    private void Start()
    {
        print("Food manager start being called");
        GameManager.instance.messageSystem.Subscribe(MessageType.NEW_FOOD, this);
        GameManager.instance.messageSystem.Subscribe(MessageType.ATE_FOOD, this);
        GameManager.instance.messageSystem.Subscribe(MessageType.FOOD_EATEN, this);
    }

    public void Receive(IMessage message)
    {
        switch (message.GetMessageType())
        {
            case MessageType.NEW_FOOD:
                print("new food");
                SpawnFood((FoodUpdate)message);
                break;
            case MessageType.FOOD_EATEN:
                FoodEaten eatenMessage = (FoodEaten)message;
                RemoveFood(eatenMessage.foodID);
                break;
            case MessageType.ATE_FOOD:
                AteFood ateMessage = (AteFood)message;
                RemoveFood(ateMessage.foodID);
                break;
        }
    }

    void SpawnFood(FoodUpdate food)
    {
        var newFood = Instantiate(foodPrefab, transform);
        newFood.name = "Food " + food.id;
        foodObjects.Add(food.id, newFood);

        newFood.transform.position = food.pos;        
    }

    void RemoveFood(int id)
    {
        if (foodObjects.ContainsKey(id))
        {
            GameObject toRemove = foodObjects[id];
            foodObjects.Remove(id);
            Destroy(toRemove);
        }
    }
}
