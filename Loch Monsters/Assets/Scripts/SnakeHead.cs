using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnakeHead : MonoBehaviour
{
    public float speed = 5;
    public float dist = 1;


    Vector3 lastDir = new Vector3(0, 0, 0);
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
        //Only send update if direction has changed, may want to send at a fixed interval as a keep alive message too
        if(transform.up != lastDir)
        {
            NetworkManagerScript.instance.SendPosUpdate(transform.position, transform.right);
        }
       
        lastDir = transform.up;

        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector2 der = (mousePosition - transform.position);

        
        //Debug.Log("transform.rotation.eulerAngles" + transform.rotation.eulerAngles + "der" + der);
        //Debug.Log("Introplated ANgle"+Vector2.Lerp(transform.rotation.eulerAngles, der, Time.deltaTime));
        
        if(der.magnitude > 1f)
        {
            transform.up = Vector2.Lerp(transform.rotation.eulerAngles, der, Time.deltaTime * .01f);
        }


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
