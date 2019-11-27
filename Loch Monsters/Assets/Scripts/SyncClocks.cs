using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//VTC would fail this class lol
//Dispatches a clock sync event every 5 seconds
public class SyncClocks : MonoBehaviour, IMessageListener
{
    const float CLOCK_SYNC_INTERVAL = 5;

    public void Receive(IMessage message)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    


}
