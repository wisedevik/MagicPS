using MagicPS.Server.Network;
using MagicPS.Titan.Message;
using MagicPS.Titan.RC4;
using Microsoft.Extensions.Logging;

namespace MagicPS.Server.Protocol;

internal class Messaging : IMessaging
{
    private const int HEADER_SIZE = 7;

    private readonly ILogger _logger;
    private readonly LogicMessageFactory _factory;

    private IMessaging.SendCallback? _sendCallback;
    private IMessaging.ReceiveCallback? _receiveCallback;

    private RC4Encrypter? _encrypter;
    private RC4Encrypter? _decrypter;

    public Messaging(LogicMessageFactory factory, ILogger<Messaging> logger)
    {
        _logger = logger;
        _factory = factory;

        _decrypter = new RC4Encrypter("fhsd6f86f67rt8fw78fw789we78r9789wer6re", "nonce");
        _encrypter = new RC4Encrypter("fhsd6f86f67rt8fw78fw789we78r9789wer6re", "nonce");
    }

    public IMessaging.SendCallback OnSend { set { _sendCallback = value; } }
    public IMessaging.ReceiveCallback RecvCallback { set { _receiveCallback = value; } }

    public async ValueTask<int> OnReceive(Memory<byte> buffer, int size)
    {
        int consumedBytes = 0;

        while (size >= HEADER_SIZE)
        {
            ReadHeader(buffer.Span, out int messageType, out int messageLength, out int messageVersion);
            if (size < HEADER_SIZE + messageLength) break;

            size -= messageLength + HEADER_SIZE;
            consumedBytes += messageLength + HEADER_SIZE;

            byte[] encryptedBytes = buffer.Slice(HEADER_SIZE, messageLength).ToArray();
            buffer = buffer[consumedBytes..];

            _decrypter.Decrypt(encryptedBytes, encryptedBytes, encryptedBytes.Length);

            PiranhaMessage? message = _factory.CreateMessageByType(messageType);
            if (message == null)
            {
                _logger.LogWarning("Ignoring message of unknown type {messageType}", messageType);
                continue;
            }

            message.SetMessageVersion(messageVersion);
            message.GetByteStream().SetByteArray(encryptedBytes, messageLength);
            message.Decode();

            await _receiveCallback!(message);

            //_logger.LogInformation("New message with type {type}", messageType);
        }

        return consumedBytes;
    }

    public async Task Send(PiranhaMessage message)
    {
        message.Encode();

        byte[] encodingBytes = message.GetByteStream().GetByteArray()!.Take(message.GetEncodingLength()).ToArray();

        _encrypter.Encrypt(encodingBytes, encodingBytes, encodingBytes.Length);

        byte[] payload = new byte[encodingBytes.Length + HEADER_SIZE];

        WriteHeader(payload, message, encodingBytes.Length);
        encodingBytes.CopyTo(payload, HEADER_SIZE);

        await _sendCallback!(payload);

        _logger.LogInformation("Message with type {type} sent", message.GetMessageType());
    }

    private static void ReadHeader(ReadOnlySpan<byte> buffer, out int messageType, out int encodingLength, out int messageVersion)
    {
        messageType = buffer[0] << 8 | buffer[1];
        encodingLength = buffer[2] << 16 | buffer[3] << 8 | buffer[4];
        messageVersion = buffer[5] << 8 | buffer[6];
    }

    private static void WriteHeader(Span<byte> buffer, PiranhaMessage message, int length)
    {
        int messageType = message.GetMessageType();
        int messageVersion = message.GetMessageVersion();

        buffer[0] = (byte)(messageType >> 8);
        buffer[1] = (byte)messageType;
        buffer[2] = (byte)(length >> 16);
        buffer[3] = (byte)(length >> 8);
        buffer[4] = (byte)length;
        buffer[5] = (byte)(messageVersion >> 8);
        buffer[6] = (byte)messageVersion;
    }
}
