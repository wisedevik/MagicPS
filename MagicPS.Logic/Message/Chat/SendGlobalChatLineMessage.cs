using MagicPS.Logic.Message.Attributes;
using MagicPS.Titan.Message;

namespace MagicPS.Logic.Message.Chat;

[PiranhaMessageAttribute(14715)]
public class SendGlobalChatLineMessage : PiranhaMessage
{
    public string Message { get; private set; }

    public SendGlobalChatLineMessage() : base(0)
    {
    }

    public override void Decode()
    {
        base.Decode();
        Message = m_stream.ReadString();
    }

    public override short GetMessageType()
    {
        return 14715;
    }

    public override int GetServiceNodeType()
    {
        return 6;
    }
}
