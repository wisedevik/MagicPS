using MagicPS.Titan.DataStream;

namespace MagicPS.Logic.Base;

public class LogicBase
{
    private int _logicDataVersion = 0;

    public virtual void Encode(ChecksumEncoder encoder)
    {
        encoder.WriteInt(_logicDataVersion);
    }
}
