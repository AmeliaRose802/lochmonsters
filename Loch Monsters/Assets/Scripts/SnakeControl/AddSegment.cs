using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSegment : MonoBehaviour
{
    public GameObject segmentPrefab;
    SnakeHead snakeHeadScript;

    // Start is called before the first frame update
    void Start()
    {
        snakeHeadScript = GetComponent<SnakeHead>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "food")
        {

            string name = collision.gameObject.name;
            

            int id = int.Parse(name.Split(' ')[1]);
            if (FoodManager.instance.foodObjects.ContainsKey(id))
            {
                FoodManager.instance.foodObjects.Remove(id);
                Debug.Log("Picked up food " + name);

                Destroy(collision.gameObject);
                AteFood ateFood = new AteFood(id);
                GameManager.instance.messageSystem.DispatchMessage(ateFood);
                snakeHeadScript.segments.Add(Instantiate(segmentPrefab, snakeHeadScript.segments[snakeHeadScript.segments.Count - 1].transform.position, snakeHeadScript.segments[snakeHeadScript.segments.Count - 1].transform.rotation));
            }  

        }
        
    }
    
}
