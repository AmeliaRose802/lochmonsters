public class AddPlayerSegment : IMessage
{
    public MessageType GetMessageType()
    {
        return MessageType.ADD_PLAYER_SEGMENT;
    }
}