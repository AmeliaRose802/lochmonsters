public interface IMessage
{
    MessageType GetMessageType();
}

public interface INetworkMessage
{
    byte[] GetMessage();
}
