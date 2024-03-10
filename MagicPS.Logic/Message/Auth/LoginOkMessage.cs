using MagicPS.Logic.Message.Attributes;
using MagicPS.Titan.Logic;
using MagicPS.Titan.Message;
using System.IO;

namespace MagicPS.Logic.Message.Auth;

[PiranhaMessageAttribute(20104)]
public class LoginOkMessage : PiranhaMessage
{
    public LogicLong AccountId { get; set; }
    public LogicLong HomeId { get; set; }
    public string PassToken { get; set; }
    public string FacebookId { get; set; }
    public string GamecenterId { get; set; }
    public int ServerMajorVersion { get; set; }
    public int ServerBuild { get; set; }
    public int ContentVersion { get; set; }
    public string ServerEnvironment { get; set; }
    public int SessionCount { get; set; }
    public int PlayTimeSeconds { get; set; }
    public int DaysSinceStartedPlaying { get; set; }
    public string FacebookAppId { get; set; }
    public string ServerTime { get; set; }
    public string AccountCreatedDate { get; set; }
    public int StartupCooldownSeconds { get; set; }
    public string GoogleServiceId { get; set; }

    public LoginOkMessage() : base(0)
    {
    }

    public override void Encode()
    {
        base.Encode();
        m_stream.WriteLong(AccountId);
        m_stream.WriteLong(HomeId);
        m_stream.WriteString(PassToken);
        m_stream.WriteString(FacebookAppId);
        m_stream.WriteString(GamecenterId);
        m_stream.WriteInt(ServerMajorVersion);
        m_stream.WriteInt(ServerBuild);
        m_stream.WriteInt(ContentVersion);
        m_stream.WriteString(ServerEnvironment);
        m_stream.WriteInt(SessionCount);
        m_stream.WriteInt(PlayTimeSeconds);
        m_stream.WriteInt(DaysSinceStartedPlaying);
        m_stream.WriteString(FacebookAppId);
        m_stream.WriteString(ServerTime);
        m_stream.WriteString(AccountCreatedDate);
        m_stream.WriteInt(StartupCooldownSeconds);
        m_stream.WriteString(GoogleServiceId);
    }

    public override short GetMessageType()
    {
        return 20104;
    }

    public override int GetServiceNodeType()
    {
        return 1;
    }
}
