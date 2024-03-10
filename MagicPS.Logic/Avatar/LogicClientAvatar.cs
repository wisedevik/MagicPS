using MagicPS.Logic.Base;
using MagicPS.Titan.DataStream;

namespace MagicPS.Logic.Avatar;

public class LogicClientAvatar : LogicBase
{
    private int[] _resources = { 3000001, 3000002, 3000003 };
    private int[] _tutorialSteps = Enumerable.Range(21000000, 13).ToArray();

    public string Name { get; set; }
    public int Diamonds { get; set; }

    public override void Encode(ChecksumEncoder encoder)
    {
        base.Encode(encoder);
        encoder.WriteLong(1);
        encoder.WriteLong(1);
        encoder.WriteBoolean(false);
        encoder.WriteBoolean(false);
        encoder.WriteBoolean(false);
        encoder.WriteInt(0);
        encoder.WriteInt(0);
        encoder.WriteInt(0);
        encoder.WriteInt(0);
        encoder.WriteInt(0);
        encoder.WriteString(Name);
        encoder.WriteString("");
        encoder.WriteInt(1);
        encoder.WriteInt(0);
        encoder.WriteInt(Diamonds);
        encoder.WriteInt(0);
        encoder.WriteInt(0);
        encoder.WriteInt(0);
        encoder.WriteInt(0);
        encoder.WriteInt(0);
        encoder.WriteInt(0);
        encoder.WriteInt(0);
        encoder.WriteInt(0);
        encoder.WriteBoolean(false);
        encoder.WriteInt(0);


        encoder.WriteInt(0);
        encoder.WriteInt(_resources.Length);
        foreach (int item in _resources)
        {
            encoder.WriteInt(item);
            encoder.WriteInt(int.MaxValue);
        }

        encoder.WriteInt(0);
        encoder.WriteInt(0);
        encoder.WriteInt(0);
        encoder.WriteInt(0);
        encoder.WriteInt(0);
        encoder.WriteInt(0);
        encoder.WriteInt(0);
        encoder.WriteInt(0);

        encoder.WriteInt(_tutorialSteps.Length);
        foreach (int item in _tutorialSteps)
        {
            encoder.WriteInt(item);
        }

        encoder.WriteInt(0);
        encoder.WriteInt(0);
        encoder.WriteInt(0);
        encoder.WriteInt(0);
        encoder.WriteInt(0);
    }
}
