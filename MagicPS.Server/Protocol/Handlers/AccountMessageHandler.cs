using MagicPS.Logic.Avatar;
using MagicPS.Logic.Home;
using MagicPS.Logic.Message.Auth;
using MagicPS.Logic.Message.Home;
using MagicPS.Server.Network.Connection;
using MagicPS.Server.Protocol.Attributes;
using Microsoft.Extensions.Logging;

namespace MagicPS.Server.Protocol.Handlers;

[ServiceNode(1)]
internal class AccountMessageHandler : MessageHandlerBase
{
    private readonly ILogger _logger;
    private readonly ClientConnection _connection;

    public AccountMessageHandler(ClientConnection connection, ILogger<AccountMessageHandler> logger)
    {
        _logger = logger;
        _connection = connection;
    }

    [MessageHandler(10101)]
    public async Task OnLoginMessage(LoginMessage loginMessage)
    {
        _logger.LogInformation("Logged in! AccountId: {aid}, PassToken: {passToken}, ClientMajorVersion: {clientMajor}, ClientBuild: {clientBuild}, ResourceSha: {resourceSha}, UDID: {udid}, OpenUDID: {openUdid}, MacAddress: {macAddress}, Device: {device}, PreferredLanguage: {preferredLanguage}, PreferredDeviceLanguage: {preferredDeviceLanguage}, ADID: {adid}, OSVersion: {osVersion}, isAndroid: {isAndroid}",
                loginMessage.AccountId.ToString(), loginMessage.PassToken, loginMessage.ClientMajorVersion, loginMessage.ClientBuild, loginMessage.ResourceSha, loginMessage.UDID, loginMessage.OpenUDID, loginMessage.MacAddress, loginMessage.Device, loginMessage.PreferredLanguage, loginMessage.PreferredDeviceLanguage, loginMessage.ADID, loginMessage.OSVersion, loginMessage.isAndroid);

        await _connection.SendMessage(new LoginOkMessage()
        {
            AccountId = 1,
            HomeId = 1,
            PassToken = string.Empty,
            FacebookId = string.Empty,
            GamecenterId = string.Empty,
            ServerMajorVersion = 5,
            ServerBuild = 2,
            ContentVersion = 4,
            ServerEnvironment = "dev",
            SessionCount = 0,
            PlayTimeSeconds = 0,
            DaysSinceStartedPlaying = 0,
            FacebookAppId = string.Empty,
            ServerTime = string.Empty,
            AccountCreatedDate = string.Empty,
            StartupCooldownSeconds = 0,
            GoogleServiceId = string.Empty
        });

        await _connection.SendMessage(new OwnHomeDataMessage()
        {
            LogicClientHome = new LogicClientHome()
            {
                Id = 1,
                HomeJSON = "{\"buildings\":[{\"data\":1000001,\"lvl\":0,\"x\":21,\"y\":20},{\"data\":1000004,\"lvl\":0,\"x\":20,\"y\":16,\"res_time\":8992},{\"data\":1000000,\"lvl\":0,\"x\":26,\"y\":19,\"units\":[],\"storage_type\":0},{\"data\":1000015,\"lvl\":0,\"x\":18,\"y\":20},{\"data\":1000014,\"lvl\":0,\"locked\":true,\"x\":25,\"y\":32}],\"obstacles\":[{\"data\":8000007,\"x\":5,\"y\":13},{\"data\":8000007,\"x\":15,\"y\":29},{\"data\":8000008,\"x\":7,\"y\":7},{\"data\":8000005,\"x\":29,\"y\":4},{\"data\":8000006,\"x\":15,\"y\":37},{\"data\":8000000,\"x\":20,\"y\":4},{\"data\":8000008,\"x\":15,\"y\":22},{\"data\":8000005,\"x\":37,\"y\":18},{\"data\":8000007,\"x\":6,\"y\":4},{\"data\":8000003,\"x\":26,\"y\":10},{\"data\":8000004,\"x\":21,\"y\":9},{\"data\":8000008,\"x\":32,\"y\":21},{\"data\":8000005,\"x\":20,\"y\":36},{\"data\":8000003,\"x\":29,\"y\":34},{\"data\":8000005,\"x\":5,\"y\":29},{\"data\":8000005,\"x\":8,\"y\":10},{\"data\":8000005,\"x\":5,\"y\":17},{\"data\":8000002,\"x\":4,\"y\":33},{\"data\":8000002,\"x\":5,\"y\":21},{\"data\":8000002,\"x\":10,\"y\":32},{\"data\":8000008,\"x\":5,\"y\":37},{\"data\":8000001,\"x\":9,\"y\":4},{\"data\":8000001,\"x\":13,\"y\":31},{\"data\":8000001,\"x\":7,\"y\":35},{\"data\":8000007,\"x\":4,\"y\":9},{\"data\":8000004,\"x\":9,\"y\":23},{\"data\":8000004,\"x\":6,\"y\":26},{\"data\":8000003,\"x\":35,\"y\":21},{\"data\":8000005,\"x\":32,\"y\":28},{\"data\":8000005,\"x\":34,\"y\":13},{\"data\":8000001,\"x\":14,\"y\":18},{\"data\":8000001,\"x\":35,\"y\":5},{\"data\":8000012,\"x\":24,\"y\":30},{\"data\":8000012,\"x\":31,\"y\":10},{\"data\":8000010,\"x\":26,\"y\":38},{\"data\":8000010,\"x\":14,\"y\":5},{\"data\":8000013,\"x\":34,\"y\":33},{\"data\":8000013,\"x\":13,\"y\":9},{\"data\":8000014,\"x\":10,\"y\":17},{\"data\":8000014,\"x\":24,\"y\":7},{\"data\":8000006,\"x\":36,\"y\":26},{\"data\":8000011,\"x\":23,\"y\":34},{\"data\":8000011,\"x\":24,\"y\":37},{\"data\":8000000,\"x\":27,\"y\":35},{\"data\":8000000,\"x\":25,\"y\":35},{\"data\":8000000,\"x\":26,\"y\":30},{\"data\":8000007,\"x\":23,\"y\":32},{\"data\":8000001,\"x\":28,\"y\":31},{\"data\":8000014,\"x\":28,\"y\":29}],\"traps\":[],\"decos\":[],\"respawnVars\":{\"secondsFromLastRespawn\":0,\"respawnSeed\":1529463799,\"obstacleClearCounter\":0},\"cooldowns\":[]}",
                ShieldDurationSeconds = 0,
                DefenseRating = 0,
                DefenseKFactor = 0
            },
            LogicClientAvatar = new LogicClientAvatar()
            {
                Name = "bladewise",
                Diamonds = int.MaxValue
            }
        });
    }
}
