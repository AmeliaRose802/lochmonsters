using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestClockSync : IMessage
{
    public MessageType GetMessageType()
    {
        return MessageType.REQUEST_CLOCK_SYNC;
    }
}
