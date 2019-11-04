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

    // Update is called once per frame
    void Update()
    {
        
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "food")
        {
            Destroy(collision.gameObject);
            snakeHeadScript.segments.Add(Instantiate(segmentPrefab, snakeHeadScript.segments[snakeHeadScript.segments.Count -1].transform.position, snakeHeadScript.segments[snakeHeadScript.segments.Count - 1].transform.rotation));
        }
        
    }
    
}
