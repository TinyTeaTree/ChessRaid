using System.Data.Common;

public class TurnBox : DataBox
{
    public const string BoxId = "Turn";
    public override string Id => BoxId;

    public int Turn = 3;
}