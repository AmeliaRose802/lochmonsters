using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ClockSyncMessage :  NetworkMessage
{
    public long timestamp;

    public ClockSyncMessage(){}

    public ClockSyncMessage(byte [] timeReply, long latency)
    {
        timestamp = BitConverter.ToInt64(timeReply, 0) + latency/2;
        
    }

    public void SetClock()
    {
        GameManager.instance.gameTime = timestamp;
    }

    //This is probley going to eventally need to contain more data so mine as well build the infrastructure now
    public byte[] GetMessage()
    {
        List<byte> message = new List<byte>();
        message.AddRange(BitConverter.GetBytes('t'));
        return message.ToArray();
    }
}
