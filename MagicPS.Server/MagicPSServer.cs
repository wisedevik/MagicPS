using MagicPS.Server.Network.Tcp;
using MagicPS.Titan.Debug;
using Microsoft.Extensions.Hosting;

namespace MagicPS.Server;

internal class MagicPSServer : IHostedService
{
    private readonly TcpGateway _gateway;

    public MagicPSServer(TcpGateway gateway, IDebuggerListener listener)
    {
        _gateway = gateway;

        Debugger.SetListener(listener);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _gateway.Start();

        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _gateway.StopAsync();
    }
}
