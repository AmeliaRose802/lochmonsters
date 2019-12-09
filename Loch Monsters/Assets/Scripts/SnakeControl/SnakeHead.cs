using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnakeHead : MonoBehaviour
{
    public string playerName;
    public Color playerColor;

    bool shouldFollow = true;

    Vector3 lastDir = new Vector3(0, 0, 0);

    Rigidbody2D rb;

    public List<GameObject> segments;
    public SnakeData snakeData;
    public float headScale = 1;
    float scaleStep = .33f;
    float minScale = .5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * GlobalConsts.SNAKE_SPEED ;
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

        transform.position = GlobalConsts.ClampToRange(transform.position, GlobalConsts.FIELD_SIZE);
        


        if (segments.Count > 200)
        {
            Debug.Log("You win!");
            MessageSystem.instance.DispatchMessage(new EndGame());
        }
    }

    void FollowMouse()
    {
        
            //Follow the mouse
       Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

       Vector2 der = (mousePosition - transform.position);
        transform.up = der;
    }

   
    public void Resize()
    {

        headScale = 1 + GlobalConsts.SCALE_STEP * Mathf.Max(0, segments.Count - 3);
        gameObject.transform.localScale = new Vector3(headScale, headScale, 1);

        scaleStep = headScale / segments.Count;
        minScale = Mathf.Max(.5f, headScale / 4f);

        for (int i = 0; i < segments.Count; i++)
        {
            float scale = Mathf.Max(minScale, headScale - (scaleStep * (i + 2)));
            segments[i].transform.localScale = new Vector3(scale, scale, 1);
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

            float scale = Mathf.Max(minScale, headScale - (scaleStep * (i + 2)));

            if (i == 0)
            {
                segment.transform.position = targetS + (GlobalConsts.SEGMENT_DIST * scale) * diff* 1.8f;
            }
            else
            {
                segment.transform.position = targetS + (GlobalConsts.SEGMENT_DIST * scale) * diff;
            }

            
            
        }
    }
}



