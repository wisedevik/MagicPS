using MagicPS.Server.Network.Connection;
using MagicPS.Server.Option;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Sockets;

namespace MagicPS.Server.Network.Tcp;

internal class TcpGateway
{
    private const int TCP_BACK_LOG = 100;

    private readonly ILogger _logger;
    private readonly IOptions<GatewayOptions> _options;
    private readonly ClientConnectionManager _connectionManager;
    private Socket _socket;

    private CancellationTokenSource? _listenCancellation;
    private Task? _listenTask;

    public TcpGateway(IOptions<GatewayOptions> options, ILogger<TcpGateway> logger, ClientConnectionManager clientConnection)
    {
        _options = options;
        _logger = logger;
        _connectionManager = clientConnection;

        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    public void Start()
    {
        _socket.Bind(_options.Value.IPEndPoint);
        _socket.Listen(TCP_BACK_LOG);

        _listenCancellation = new CancellationTokenSource();
        _listenTask = RunAsync(_listenCancellation.Token);

        _logger.LogInformation("Server is listening at {ip}", _options.Value.IPEndPoint);
    }

    private async Task RunAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            Socket? socket = await _socket.AcceptAsync(cancellationToken);
            if (socket == null) break;

            _connectionManager.OnConnect(new TcpSocketEntity(socket));
        }
    }

    public async Task StopAsync()
    {
        if (_connectionManager != null)
        {
            await _listenCancellation.CancelAsync();
            await _listenTask!;
        }

        _socket.Close();
    }
}
