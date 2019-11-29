using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTarget : MonoBehaviour
{ 
    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.up * GlobalConsts.SNAKE_SPEED;
        transform.position = GlobalConsts.ClampToRange(transform.position, new Vector2(25, 25));
    }
}
