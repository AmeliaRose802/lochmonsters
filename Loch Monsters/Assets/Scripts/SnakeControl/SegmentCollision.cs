using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "otherPlayer")
        {
            
            int id = int.Parse(collision.gameObject.transform.parent.name.Split(':')[0].Trim());

            //Prevent sibiling collsions from regestering
            if(id != GameManager.instance.id)
            {
                MessageSystem.instance.DispatchMessage(new HitBySnake(id));
            }
            
        }
    }
}
