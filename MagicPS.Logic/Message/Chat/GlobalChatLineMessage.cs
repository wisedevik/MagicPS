using MagicPS.Logic.Message.Attributes;
using MagicPS.Titan.Logic;
using MagicPS.Titan.Message;

namespace MagicPS.Logic.Message.Chat;

[PiranhaMessageAttribute(24715)]
public class GlobalChatLineMessage : PiranhaMessage
{
    public string Message { get; set; }
    public string AvatarName { get; set; }
    public int AvatarExpLevel { get; set; }
    public int AvatarLeagueType { get; set; }
    public LogicLong AvatarId { get; set; }
    public LogicLong HomeId { get; set; }
    public LogicLong AllianceId { get; set; }
    public string AllianceName { get; set; }
    public int AllianceBadgeData { get; set; }

    public GlobalChatLineMessage() : base(0)
    {
    }

    public override void Encode()
    {
        base.Encode();
        m_stream.WriteString(Message);
        m_stream.WriteString(AvatarName);
        m_stream.WriteInt(AvatarExpLevel);
        m_stream.WriteInt(AvatarLeagueType);
        m_stream.WriteLong(AvatarId);
        m_stream.WriteLong(HomeId);
        if (AllianceId == null)
        {
            m_stream.WriteBoolean(false);
        }
        else
        {
            m_stream.WriteBoolean(true);
            m_stream.WriteLong(AllianceId);
            m_stream.WriteString(AllianceName);
            m_stream.WriteInt(AllianceBadgeData);
        }
    }

    public override short GetMessageType()
    {
        return 24715;
    }

    public override int GetServiceNodeType()
    {
        return 6;
    }
}
