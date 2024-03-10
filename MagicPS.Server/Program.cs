using MagicPS.Logic.Message;
using MagicPS.Server;
using MagicPS.Server.Network;
using MagicPS.Server.Network.Connection;
using MagicPS.Server.Network.Tcp;
using MagicPS.Server.Option;
using MagicPS.Server.Protocol;
using MagicPS.Server.Protocol.Extensions;
using MagicPS.Titan.Debug;
using MagicPS.Titan.Message;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.Title = "MagicPS | Server";

var builder = Host.CreateApplicationBuilder();

builder.Services.Configure<GatewayOptions>(builder.Configuration.GetRequiredSection("Gateway"));

builder.Services.AddHandlers();
builder.Services.AddSingleton<IDebuggerListener, DebuggerListener>();
builder.Services.AddSingleton<TcpGateway>();
builder.Services.AddSingleton<ClientConnectionManager>();
builder.Services.AddSingleton<LogicMessageFactory, LogicMagicMessageFactory>();

builder.Services.AddScoped<ClientConnection>();
builder.Services.AddScoped<IMessaging, Messaging>();
builder.Services.AddScoped<MessageManager>();

builder.Services.AddHostedService<MagicPSServer>();

await builder.Build().RunAsync();