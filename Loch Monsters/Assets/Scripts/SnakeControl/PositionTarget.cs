using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTarget : MonoBehaviour
{
    float speed = 5;
    
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.up * speed;

        transform.position = ClampToRange(transform.position, new Vector2(25, 25));
    }


    Vector2 ClampToRange(Vector2 val, Vector2 range)
    {
        Vector2 clamped = val;
        if (val.x < -range.x)
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
