namespace ChessRaid
{
    public static class BattleEventBus
    {
        public static WagEvent OnSelectionChanged = new("On Selection Changed");
    }
}