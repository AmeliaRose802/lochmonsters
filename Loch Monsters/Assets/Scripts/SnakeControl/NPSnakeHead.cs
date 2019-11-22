using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPSnakeHead : MonoBehaviour
{
    public float speed = 5;
    public float dist = 1;

    Vector3 dir = new Vector3(0, 0, 0);
    Rigidbody2D rb;

    public List<GameObject> segments;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

    }

    // Move the segment foward
    void FixedUpdate()
    {

        rb.velocity = transform.up * speed;

        //Thanks to https://www.reddit.com/r/Unity2D/comments/7hwxfk/how_do_i_make_a_tail_like_a_snake/ for this code
        for (int i = 0; i < segments.Count; i++)
        {
            var segment = segments[i];
            Vector3 positionS = segments[i].transform.position;
            Vector3 targetS = i == 0 ? transform.position : segments[i - 1].transform.position;
            segments[i].transform.rotation = Quaternion.LookRotation(Vector3.forward, (targetS - positionS).normalized);
            Vector3 diff = positionS - targetS;  //vector pointing from p[i - 1] to p[i]
            diff.Normalize();
            segment.transform.position = targetS + dist * diff;
        }

    }

    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan((a.y - b.y) / a.x - b.x) * Mathf.Rad2Deg;
    }
}
