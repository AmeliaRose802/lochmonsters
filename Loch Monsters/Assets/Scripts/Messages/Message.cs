public interface IMessage
{
    MessageType GetMessageType();
}

public interface NetworkMessage
{
    byte[] GetMessage();
}
