using System.Collections.Generic;

namespace ChessRaid
{
    [System.Serializable]
    public class GridState
    {
        [System.Serializable]
        public class ChampionPosition
        {
            public string ChampionId;
            public Direction Direction;
            public Coord Location;
            public Team Team;
        }

        public List<ChampionPosition> Board;
    }
}