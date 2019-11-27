using UnityEngine;
using System.Collections;

public class TerminationMessage : IMessage
{
    public MessageType GetMessageType()
    {
        return MessageType.TERMINATION_MESSAGE;
    }
}