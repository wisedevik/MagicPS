using MagicPS.Logic.Message.Chat;
using MagicPS.Server.Network.Connection;
using MagicPS.Server.Protocol.Attributes;
using Microsoft.Extensions.Logging;

namespace MagicPS.Server.Protocol.Handlers;

[ServiceNode(6)]
internal class ChatMessageHandler : MessageHandlerBase
{
    private readonly ILogger _logger;
    private readonly ClientConnection _connection;

    public ChatMessageHandler(ILogger<ChatMessageHandler> logger, ClientConnection clientConnection)
    {
        _logger = logger;
        _connection = clientConnection;
    }

    [MessageHandler(14715)]
    public async Task OnSendGlobalChatLineMessage(SendGlobalChatLineMessage sendGlobalChatLineMessage)
    {
        _logger.LogInformation("ChatMessageHandler.OnSendGlobalChatLineMessage: {message}", sendGlobalChatLineMessage.Message);

        await _connection.SendMessage(new GlobalChatLineMessage()
        {
            Message = sendGlobalChatLineMessage.Message,
            AvatarName = "bladewise",
            AvatarExpLevel = 0,
            AvatarLeagueType = 0,
            AvatarId = 1,
            HomeId = 1
        });
    }
}
