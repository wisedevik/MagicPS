using System.Net;

namespace MagicPS.Server.Option;
internal class GatewayOptions
{
    public required string Host { get; set; }
    public required int Port { get; set; }

    public IPEndPoint IPEndPoint => new(IPAddress.Parse(Host), Port);
}

