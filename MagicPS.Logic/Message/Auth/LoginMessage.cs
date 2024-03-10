using MagicPS.Logic.Message.Attributes;
using MagicPS.Titan.Logic;
using MagicPS.Titan.Message;
using System.IO;

namespace MagicPS.Logic.Message.Auth;

[PiranhaMessageAttribute(10101)]
public class LoginMessage : PiranhaMessage
{
    public LogicLong AccountId; //this + 48
    public string PassToken;
    public int ClientMajorVersion;
    public int ClientBuild;
    public string ResourceSha;
    public string UDID;
    public string OpenUDID;
    public string MacAddress;
    public string Device;
    public int PreferredLanguage;
    public string PreferredDeviceLanguage;
    public string ADID;
    public string OSVersion;
    public bool isAndroid;
    public string IMEI;
    public string AndroidID;

    public LoginMessage() : base(0)
    {
        AccountId = 0;
        PassToken = string.Empty;
        ClientMajorVersion = -1;
        ClientBuild = -1;
        ResourceSha = string.Empty;
        UDID = string.Empty;
        OpenUDID = string.Empty;
        MacAddress = string.Empty;
        Device = string.Empty;
        PreferredLanguage = 0;
        PreferredDeviceLanguage = string.Empty;
        ADID = string.Empty;
        OSVersion = string.Empty;
        isAndroid = false;
        IMEI = string.Empty;
        AndroidID = string.Empty;
    }

    public override void Decode()
    {
        base.Decode();

        AccountId = m_stream.ReadLong();
        PassToken = m_stream.ReadString();
        ClientMajorVersion = m_stream.ReadInt();
        m_stream.ReadInt(); //суперсел че за
        ClientBuild = m_stream.ReadInt();
        ResourceSha = m_stream.ReadString();
        UDID = m_stream.ReadString();
        OpenUDID = m_stream.ReadString();
        MacAddress = m_stream.ReadString();
        Device = m_stream.ReadString();
        if (!m_stream.IsAtEnd())
        {
            PreferredLanguage = m_stream.ReadInt();
            PreferredDeviceLanguage = m_stream.ReadString();

            if (!m_stream.IsAtEnd())
            {
                ADID = m_stream.ReadString();
                if (!m_stream.IsAtEnd())
                {
                    OSVersion = m_stream.ReadString();
                    if (!m_stream.IsAtEnd())
                    {
                        isAndroid = m_stream.ReadBoolean();
                        if (!m_stream.IsAtEnd())
                        {
                            IMEI = m_stream.ReadStringReference(900000);
                            AndroidID = m_stream.ReadStringReference(900000);
                        }
                    }
                }
            }
        }
    }

    public override short GetMessageType()
    {
        return 10101;
    }

    public override int GetServiceNodeType()
    {
        return 1;
    }
}
