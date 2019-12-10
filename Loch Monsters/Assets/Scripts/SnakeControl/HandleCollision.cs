using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollision : MonoBehaviour
{
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if(collision.tag == "food")
        {

            string name = collision.gameObject.name;
            
            int id = int.Parse(name.Split(' ')[1]);
            
            PlayerAteFood ateFood = new PlayerAteFood(id);
            MessageSystem.instance.DispatchMessage(ateFood);
        }
        if (collision.tag == "segment")
        {
            if (collision.gameObject.transform.parent.tag == "otherPlayer")
            {
                int id = int.Parse(collision.gameObject.transform.parent.name.Split(':')[0].Trim());
                MessageSystem.instance.DispatchMessage(new HitSnake(id));
            }
        }

        if (collision.gameObject.tag == "otherPlayer")
        {
            int id = int.Parse(collision.gameObject.transform.parent.name.Split(':')[0].Trim());
            Debug.Log("Hit head of: " + collision.gameObject.transform.parent.name);

            MessageSystem.instance.DispatchMessage(new HitSnake(id));
        }
    }
}
