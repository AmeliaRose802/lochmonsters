using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TimeMessage : INetworkMessage
{
    public long sendTime;
    public long serverTime;
    public long latency;

    public TimeMessage()
    {
        sendTime = GameManager.instance.gameTime;
    }

    public void SetGameTime(byte[] message)
    {
        serverTime = BitConverter.ToInt64(message, 0);

        //Get latency based on the local game clock before resetting itS
        latency = GameManager.instance.gameTime - sendTime;

        GameManager.instance.gameTime = serverTime + (latency / 2);
    }


    public byte[] GetMessage()
    {
        List<byte> packet = new List<byte>();
        packet.AddRange(BitConverter.GetBytes('t'));

        return packet.ToArray();
    }
}
