using System.Net;
using System.Net.Sockets;

namespace MagicPS.Server.Network.Tcp;

internal class TcpSocketEntity
{
    private readonly Socket _socket;

    public TcpSocketEntity(Socket socket)
    {
        _socket = socket;
    }

    public EndPoint RemoteEndPoint => _socket.RemoteEndPoint;

    public void Dispose()
    {
        _socket.Close();
    }

    public ValueTask<int> ReceiveAsync(Memory<byte> buffer, CancellationToken cancellationToken)
    {
        return _socket.ReceiveAsync(buffer, SocketFlags.None, cancellationToken);
    }

    public ValueTask<int> SendAsync(Memory<byte> buffer, CancellationToken cancellationToken)
    {
        return _socket.SendAsync(buffer, SocketFlags.None, cancellationToken);
    }
}
