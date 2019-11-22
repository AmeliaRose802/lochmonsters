using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Thanks to https://learn.unity.com/tutorial/movement-basics?projectId=5c514956edbc2a002069467c#5c7f8528edbc2a002053b711 for this script
    public Transform player;


    private Vector3 offset;

    // Use this for initialization
    void Awake()
    {
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}
