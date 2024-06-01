using System.Collections.Generic;

namespace ChessRaid
{
    public class TurnBox : DataBox
    {
        public const string BoxId = "Turn";
        public override string Id => BoxId;

        public List<TurnChain> Chains = new();
    }
}