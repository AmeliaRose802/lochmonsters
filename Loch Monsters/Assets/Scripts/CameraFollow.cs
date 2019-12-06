using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Thanks to https://learn.unity.com/tutorial/movement-basics?projectId=5c514956edbc2a002069467c#5c7f8528edbc2a002053b711 for this script
    public Transform player;


    private Vector3 offset = new Vector3(.5f, .5f, -10f);

    private void Start()
    {
        StartCoroutine(GetPlayer());
    }

    IEnumerator GetPlayer()
    {
        yield return new WaitForFixedUpdate(); //Wait a fixed update cycle to make sure that all new objects can init
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }


    void LateUpdate()
    {
        if(player != null)
        {
            var newPos = player.position + offset;

            var cam = gameObject.GetComponent<Camera>();
            float halfHeight = cam.orthographicSize;

            float halfWidth = halfHeight * cam.aspect;

            if ((newPos.x + halfWidth) > GlobalConsts.FIELD_SIZE.x)
            {
                newPos.x = GlobalConsts.FIELD_SIZE.x - halfWidth + 1;
            }

            if ((newPos.x - halfWidth) < -GlobalConsts.FIELD_SIZE.x)
            {
                newPos.x = -GlobalConsts.FIELD_SIZE.x + halfWidth - 1;
            }

            if ((newPos.y + halfHeight) > GlobalConsts.FIELD_SIZE.y)
            {
                newPos.y = GlobalConsts.FIELD_SIZE.y - halfHeight;
            }

            if ((newPos.y - halfHeight) < -GlobalConsts.FIELD_SIZE.y)
            {
                newPos.y = -GlobalConsts.FIELD_SIZE.y + halfHeight;
            }


            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 40f);
            //transform.position = newPos;
        }
    }
}
