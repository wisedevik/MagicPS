using MagicPS.Logic.Base;
using MagicPS.Titan.DataStream;
using MagicPS.Titan.Logic;

namespace MagicPS.Logic.Home;

public class LogicClientHome : LogicBase
{
    public LogicLong Id { get; set; }
    public string HomeJSON { get; set; }
    public int ShieldDurationSeconds { get; set; }
    public int DefenseRating { get; set; }
    public int DefenseKFactor { get; set; }

    public override void Encode(ChecksumEncoder encoder)
    {
        base.Encode(encoder);
        encoder.WriteLong(Id);
        encoder.WriteString(HomeJSON);
        encoder.WriteInt(ShieldDurationSeconds);
        encoder.WriteInt(DefenseRating);
        encoder.WriteInt(DefenseKFactor);
    }
}
