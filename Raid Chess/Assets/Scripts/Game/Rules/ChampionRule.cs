namespace ChessRaid
{
    /// <summary>
    /// This describes a single Champion Rule
    /// </summary>
    [System.Serializable]
    public class ChampionRule
    {
        public string RuleName;
        public ActionType Action;
        public RuleRange Range; 
    }
}