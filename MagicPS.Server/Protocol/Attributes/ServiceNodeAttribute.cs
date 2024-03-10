namespace MagicPS.Server.Protocol.Attributes;

[AttributeUsage(AttributeTargets.Class)]
internal class ServiceNodeAttribute : Attribute
{
    public int ServiceNodeType { get; }

    public ServiceNodeAttribute(int serviceNodeType)
    {
        ServiceNodeType = serviceNodeType;
    }
}
