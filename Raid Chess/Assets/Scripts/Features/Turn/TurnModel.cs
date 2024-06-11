namespace ChessRaid
{
    public class TurnModel : WagSingleton<TurnModel>
    {
        private RulesManager _rulesManager;
        private DataWarehouse _dataWarehouse;

        private TurnBox _box;

        public override void Awake(ContextGroup<IController> group)
        {
            _dataWarehouse = group.Get<DataWarehouse>();
            _rulesManager = group.Get<RulesManager>();
        }

        public override void Start()
        {
            _box = _dataWarehouse.GetBox<TurnBox>();
        }

        public void TryOrderAction(Hex hitHex, ActionType selectedAction, Hex selectedHex)
        {
            var selectedChampion = selectedHex.Champion;
            if (!_rulesManager.CanOrder(selectedChampion, selectedAction, hitHex))
                return;

            var championChain = _box.GetChampionChain(selectedChampion);
            championChain.AddAction(hitHex, selectedAction);
            GridManager._.MarkHexAction(hitHex, selectedAction);
        }

        public TurnChain GetTurnChain(Champion champion)
        {
            var championChain = _box.GetChampionChain(champion);
            return championChain;
        }

        public void UndoLastTurn()
        {
            var selectedChampion = SelectionManager._.SelectedHex?.Champion;
            if(selectedChampion == null)
            {
                UnityEngine.Debug.LogWarning($"Did not find any champion to undo turn");
                return;
            }

            var championChain = _box.GetChampionChain(selectedChampion);

            if(championChain == null)
            {
                UnityEngine.Debug.LogWarning($"Did not find any champion chain to undo turn");
                return;
            }

            championChain.RemoveLastActionOrdered();
        }

        public void RemoveTurnChain(Champion selectedChampion)
        {
            if (selectedChampion == null)
            {
                UnityEngine.Debug.LogWarning($"Did not find any champion to undo turn");
                return;
            }

            var championChain = _box.GetChampionChain(selectedChampion);

            if (championChain == null)
            {
                UnityEngine.Debug.LogWarning($"Did not find any champion chain to undo turn");
                return;
            }

            championChain.RemoveTurnChain();
        }

        public void RemoveAllTurnChains()
        {
            var champions = Squad._.GetChampions(Team.Home);

            foreach(var champion in champions)
            {
                RemoveTurnChain(champion);
            }
        }
    }
}