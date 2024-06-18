using System;
using System.Collections.Generic;
using UnityEngine;


namespace ChessRaid
{
    public class GridManager : WagMonoton<GridManager>
    {
        [SerializeField] Hex _hexPrefab;
        [SerializeField] Transform _hexRoot;

        List<Hex> _allHexes;
        [SerializeField] GridLevelSO _levelSO;
        [SerializeField] Transform _level;

        Dictionary<Coord, Hex> _hexMap;

        public GridState StartingState => _levelSO.StartingState;

        private void Start()
        {
            BattleEventBus.OnSelectionChanged.AddListener(OnSelectionChanged);
            BattleEventBus.TurnActionChanged.AddListener(OnSelectionChanged);
            ActionPanel._.OnBoxChanged.AddListener(OnSelectionChanged);

            _level.position = _levelSO.LevelPlacement;
            var colliders = _level.GetComponentsInChildren<BoxCollider>();
            foreach(var c in colliders)
            {
                Destroy(c);
            }
        }

        private void OnSelectionChanged()
        {
            ClearHexState();
            SetCurrentState();
        }

        public void AttachArrow(Arrow arrow)
        {
            arrow.transform.SetParent(transform);
        }

        private void SetCurrentState()
        {
            var selectedHex = SelectionManager._.SelectedHex;
            if (selectedHex == null)
                return;

            var selectedChampion = selectedHex.Champion;
            if (selectedChampion == null)
                return;

            selectedHex.ToggleSelect(true);

            if (selectedChampion.Team != Team.Home)
                return;

            var turnChain = TurnModel._.GetTurnChain(selectedChampion);
            if(turnChain == null)
            {
                Debug.LogWarning($"Was expecting a turn chain for {selectedChampion.Id}");
                return;
            }

            foreach(var turnEvent in turnChain.TurnEvents)
            {
                MarkHexAction(_hexMap[turnEvent.Location], turnEvent.Action);
            }

            MarkPossibleActionHexes(selectedChampion);
        }

        private void MarkPossibleActionHexes(Champion champion)
        {
            if (ActionPanel._.SelectedAction == ActionType.None)
                return;

            var championState = TurnModel._.GetChampionState(champion);

            var possibleTurn = RulesManager._.GetPossibleTurns(championState, ActionPanel._.SelectedAction);
            foreach(var possible in possibleTurn)
            {
                if (_hexMap.ContainsKey(possible.Location))
                {
                    _hexMap[possible.Location].ToggleIndicator(true);
                }
            }
        }

        private void ClearHexState()
        {
            foreach(var hex in _allHexes)
            {
                hex.Clear();
            }
        }

        public void SetUp()
        {
            _hexMap = new Dictionary<Coord, Hex>();

            CreateHexes();

            MapHexes();
        }

        public Hex GetHex(Coord coord)
        {
            return _hexMap[coord];
        }

        void MapHexes()
        {
            foreach (var h in _allHexes)
            {
                _hexMap.Add(h.Location, h);
            }
        }

        void CreateHexes()
        {
            _allHexes = new List<Hex>();


            foreach (var orientation in _levelSO.HexMap)
            {
                var hex = Instantiate(_hexPrefab, _hexRoot);

                hex.Location = orientation.Location;

                hex.transform.position = new Vector3(orientation.Location.X * Consts.NextHexXDistance, orientation.Height, orientation.Location.Y * Consts.NextHexYDistance + (orientation.Location.X % 2 != 0 ? Consts.OddHexYOffset : 0f));

                hex.name = $"Hex [{orientation.Location.X},{orientation.Location.Y}]";

                hex.ToggleIndicator(false);
                hex.ToggleSelect(false);

                _allHexes.Add(hex);
            }
        }

        public void MarkHexAction(Hex hitHex, ActionType selectedAction)
        {
            hitHex.SetActionSelection(selectedAction);
        }
    }
}