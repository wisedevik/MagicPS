namespace MagicPS.Server.Protocol.Attributes;

[AttributeUsage(AttributeTargets.Method)]
internal class MessageHandlerAttribute : Attribute
{
    public int MessageType { get; }

    public MessageHandlerAttribute(int messageType)
    {
        MessageType = messageType;
    }
}
