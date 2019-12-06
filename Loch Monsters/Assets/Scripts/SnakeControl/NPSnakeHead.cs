using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPSnakeHead : MonoBehaviour
{
    private Transform target;

    public List<GameObject> segments;
    public SnakeData snakeData;

    private void Start()
    {
        target = transform.GetChild(0);
    }
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * 5);
        PositionTail();
    }

    void PositionTail()
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
