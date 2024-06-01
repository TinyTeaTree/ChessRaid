using System.Collections.Generic;

namespace ChessRaid
{
    public class TurnChain
    {
        public Champion Champion;
        public List<TurnEvent> TurnEvents = new();
    }
}