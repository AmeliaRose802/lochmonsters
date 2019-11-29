

public class ConnectFailed : IMessage
{
    public string error;

    public ConnectFailed(string error)
    {
        this.error = error;
    }

    public MessageType GetMessageType()
    {
        return MessageType.CONNECT_FAILED;
    }
}
