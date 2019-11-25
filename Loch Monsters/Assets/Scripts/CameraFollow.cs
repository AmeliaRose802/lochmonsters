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
        player = SnakeManager.instance.playerHeadTranform;
    }


    void LateUpdate()
    {
        if(player != null)
        {
            transform.position = player.position + offset;
        }
    }
}
