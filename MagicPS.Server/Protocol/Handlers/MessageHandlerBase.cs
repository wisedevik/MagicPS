using MagicPS.Server.Protocol.Attributes;
using MagicPS.Titan.Message;
using System.Collections.Immutable;
using System.Reflection;

namespace MagicPS.Server.Protocol.Handlers;

internal abstract class MessageHandlerBase
{
    private readonly ImmutableDictionary<int, MethodInfo> _handlerMethods;

    public MessageHandlerBase()
    {
        var builder = ImmutableDictionary.CreateBuilder<int, MethodInfo>();

        foreach (var method in GetType().GetMethods())
        {
            MessageHandlerAttribute? attribute = method.GetCustomAttribute<MessageHandlerAttribute>();
            if (attribute == null) continue;

            builder.Add(attribute.MessageType, method);
        }

        _handlerMethods = builder.ToImmutable();
    }

    public async Task<bool> HandleMessage(PiranhaMessage message)
    {
        if (_handlerMethods.TryGetValue(message.GetMessageType(), out var method))
        {
            await (Task)method.Invoke(this, new object[] { message })!;
            return true;
        }

        return false;
    }
}
