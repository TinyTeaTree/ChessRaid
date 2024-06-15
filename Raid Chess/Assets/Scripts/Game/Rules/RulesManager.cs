namespace ChessRaid
{
    public class RulesManager : WagSingleton<RulesManager>
    {
        private DataWarehouse _dataWarehouse;
        private TurnModel _turnModel;

        private RulesBox _box;

        public override void Awake(ContextGroup<IController> group)
        {
            _dataWarehouse = group.Get<DataWarehouse>();
            _turnModel = group.Get<TurnModel>();
        }

        public override void Start()
        {
            _box = _dataWarehouse.GetBox<RulesBox>();
        }

        public bool CanOrder(Champion champion, ActionType action, Hex hitHex)
        {
            var rules = champion.Def.Rules.RuleSet.Rules;

            foreach (var rule in rules)
            {
                if (rule.Action != action)
                    continue;

                var turnOrientation = _turnModel.GetChampionTurnOrientation(champion);

                var championCoord = turnOrientation.Location;
                var orderingCoord = hitHex.Location;

                foreach (var range in rule.Range.Group)
                {
                    var ruleLocation = GridUtils.GetLocation(championCoord, turnOrientation.Direction, range);

                    if (ruleLocation == orderingCoord)
                        return true;
                }
            }

            return false;
        }
    }
}