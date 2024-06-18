namespace ChessRaid
{
    [System.Serializable]
    public class ChampionState
    {
        public Champion Champion { get; set; }
        public Direction Direction { get; set; }
        public Coord Location { get; set; }
        public Team Team { get; set; }

        public int Health { get; set; }
        public int ActionPoints { get; set; }
        public bool ActionsBlocked { get; set; }
    }
}