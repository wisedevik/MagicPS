using MagicPS.Titan.Message;

namespace MagicPS.Server.Network;

internal interface IMessaging
{
    public delegate ValueTask SendCallback(Memory<byte> buffer);
    public delegate Task ReceiveCallback(PiranhaMessage message);

    SendCallback OnSend { set; }
    ReceiveCallback RecvCallback { set; }

    ValueTask<int> OnReceive(Memory<byte> buffer, int size);

    Task Send(PiranhaMessage message);
}
