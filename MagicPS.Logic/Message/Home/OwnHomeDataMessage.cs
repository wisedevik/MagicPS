using MagicPS.Logic.Avatar;
using MagicPS.Logic.Home;
using MagicPS.Logic.Message.Attributes;
using MagicPS.Titan.Message;
using System.IO;

namespace MagicPS.Logic.Message.Home;

[PiranhaMessageAttribute(24101)]
public class OwnHomeDataMessage : PiranhaMessage
{
    public LogicClientHome LogicClientHome { get; set; }
    public LogicClientAvatar LogicClientAvatar { get; set; }
    public OwnHomeDataMessage() : base(0)
    {
    }

    public override void Encode()
    {
        base.Encode();
        m_stream.WriteInt(0);
        LogicClientHome.Encode(m_stream);
        LogicClientAvatar.Encode(m_stream);
    }
    public override short GetMessageType()
    {
        return 24101;
    }
    public override int GetServiceNodeType()
    {
        return 10;
    }
}
