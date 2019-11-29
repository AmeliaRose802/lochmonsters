using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollision : MonoBehaviour
{ 
    
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if(collision.tag == "food")
        {

            string name = collision.gameObject.name;
            
            int id = int.Parse(name.Split(' ')[1]);
            
            PlayerAteFood ateFood = new PlayerAteFood(id);
            MessageSystem.instance.DispatchMessage(ateFood);
        }
        
    }
}
