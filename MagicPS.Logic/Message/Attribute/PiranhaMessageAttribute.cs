namespace MagicPS.Logic.Message.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class PiranhaMessageAttribute(int messageType) : Attribute
{
    public int MessageType { get; } = messageType;
}