using MagicPS.Server.Network.Tcp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MagicPS.Server.Network.Connection;

internal class ClientConnectionManager
{
    private ILogger _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public ClientConnectionManager(ILogger<ClientConnectionManager> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public void OnConnect(TcpSocketEntity entity)
    {
        _logger.LogInformation("New connection from {ip}", entity.RemoteEndPoint);
        _ = RunSessionAsync(entity);
    }

    private async Task RunSessionAsync(TcpSocketEntity entity)
    {
        using IServiceScope scope = _scopeFactory.CreateScope();
        ClientConnection session = scope.ServiceProvider.GetRequiredService<ClientConnection>();

        try
        {
            session.SetProtocolEntity(entity);
            await session.RunAsync();
        }
        catch (OperationCanceledException) { }
        catch (Exception exception)
        {
            _logger.LogError("Unhandled exception occurred while processing session, trace:\n{exception}", exception);
        }
        finally
        {
            entity.Dispose();
        }
    }
}
