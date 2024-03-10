using MagicPS.Server.Network.Tcp;
using MagicPS.Server.Protocol;
using MagicPS.Titan.Message;
using System.Threading;

namespace MagicPS.Server.Network.Connection;

internal class ClientConnection
{
    private readonly IMessaging _messaging;
    private readonly MessageManager _messageManager;

    private readonly byte[] _receiveBuffer;
    private TcpSocketEntity? _entity;

    public ClientConnection(IMessaging messaging, MessageManager messageManager)
    {
        _messageManager = messageManager;

        _messaging = messaging;
        _messaging.OnSend = SendAsync;
        _messaging.RecvCallback = OnMessage;

        _receiveBuffer = GC.AllocateUninitializedArray<byte>(16384);
    }

    public async Task RunAsync()
    {
        int receiveBufferIndex = 0;
        Memory<byte> receiveBufferMem = _receiveBuffer.AsMemory();

        while (true)
        {
            int read = await ReceiveAsync(receiveBufferMem[receiveBufferIndex..]);
            if (read == 0) break;

            receiveBufferIndex += read;
            int consumedBytes = await _messaging.OnReceive(receiveBufferMem, receiveBufferIndex);

            if (consumedBytes > 0)
            {
                Buffer.BlockCopy(_receiveBuffer, consumedBytes, _receiveBuffer, 0, receiveBufferIndex -= consumedBytes);
            }
            else if (consumedBytes < 0) break;

        }
    }

    public async Task SendMessage(PiranhaMessage message)
    {
        await _messaging.Send(message);
    }

    private async Task OnMessage(PiranhaMessage message)
    {
        _messageManager.ReceiveMessage(message);
    }

    private async ValueTask SendAsync(Memory<byte> buffer)
    {
        await _entity.SendAsync(buffer, default);
    }

    private async ValueTask<int> ReceiveAsync(Memory<byte> buffer)
    {
        CancellationTokenSource cts = new(TimeSpan.FromMilliseconds(30000));
        return await _entity.ReceiveAsync(buffer, cts.Token);
    }
    public void SetProtocolEntity(TcpSocketEntity entity)
    {
        _entity = entity;
    }
}
