using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnakeHead : MonoBehaviour
{
    public string playerName;
    public Color playerColor;

    Vector3 lastDir = new Vector3(0, 0, 0);

    Rigidbody2D rb;

    public List<GameObject> segments;
    public SnakeData snakeData;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * GlobalConsts.SNAKE_SPEED;
    }

    // Move the segment foward
    void FixedUpdate()
    {
        //Only send update if direction has changed
        if (transform.up != lastDir)
        {
            MessageSystem.instance.DispatchMessage(new PositionUpdate(GameManager.instance.id, transform.position, transform.up, MessageType.SEND_PLAYER_POSITION));
            lastDir = transform.up;
        }

        FollowMouse();
        rb.velocity = transform.up * GlobalConsts.SNAKE_SPEED;
        UpdateSegments();
    }

    void FollowMouse()
    {
        //Follow the mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 der = (mousePosition - transform.position);

        if (der.magnitude > 1f)
        {
            transform.up = Vector2.Lerp(transform.rotation.eulerAngles, der, Time.deltaTime * .01f);
        }
    }
    void UpdateSegments()
    {
        //Thanks to https://www.reddit.com/r/Unity2D/comments/7hwxfk/how_do_i_make_a_tail_like_a_snake/ for this code
        for (int i = 0; i < segments.Count; i++)
        {
            var segment = segments[i];
            Vector3 positionS = segments[i].transform.position;
            Vector3 targetS = i == 0 ? transform.position : segments[i - 1].transform.position;
            segments[i].transform.rotation = Quaternion.LookRotation(Vector3.forward, (targetS - positionS).normalized);
            Vector3 diff = positionS - targetS;  //vector pointing from p[i - 1] to p[i]
            diff.Normalize();
            segment.transform.position = targetS + GlobalConsts.SEGMENT_DIST * diff;
        }
    }
}



