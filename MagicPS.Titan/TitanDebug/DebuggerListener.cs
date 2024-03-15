namespace MagicPS.Titan.Debug;

using Microsoft.Extensions.Logging;
using System;

public class DebuggerListener : IDebuggerListener
{
    private readonly ILogger _logger;

    public DebuggerListener(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger("Magic.Titan.Debug.Debugger");
    }
    public void HudPrint(string message)
    {
        _logger.LogInformation("{m}", message);
    }

    public void Print(string message)
    {
        _logger.LogInformation("{m}", message);
    }

    public void Warning(string message)
    {
        _logger.LogWarning("{m}", message);
    }

    public void Error(string message)
    {
        _logger.LogError("{m}", message);
    }
}