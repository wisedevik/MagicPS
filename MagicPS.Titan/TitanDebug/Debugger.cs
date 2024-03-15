using MagicPS.Titan.Exception;
using System.Diagnostics;

namespace MagicPS.Titan.Debug
{
    public static class Debugger
    {
        private static IDebuggerListener m_listener;

        public static bool DoAssert(bool assertion, string assertionError)
        {
            if (!assertion)
            {
                m_listener.Error(assertionError);
            }

            return assertion;
        }

        public static void HudPrint(string log)
        {
            m_listener.HudPrint(log);
        }

        public static void Print(string log)
        {
            m_listener.Print(log);
        }

        public static void Warning(string log)
        {
            m_listener.Warning(log);
        }

        public static void Error(string log)
        {
            m_listener.Error(log);
            throw new LogicException(log);
        }

        public static void SetListener(IDebuggerListener listener)
        {
            m_listener = listener;
        }
    }

    public interface IDebuggerListener
    {
        void HudPrint(string message);
        void Print(string message);
        void Warning(string message);
        void Error(string message);
    }

}
