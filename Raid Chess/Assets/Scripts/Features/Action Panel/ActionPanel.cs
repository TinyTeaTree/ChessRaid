using UnityEngine;

namespace ChessRaid
{
    public class ActionPanel : WagMonoton<ActionPanel>
    {
        [SerializeField] private ActionButton[] _allButtons;
        
        private ActionButton _selectedAction;

        public ActionType SelectedAction => _selectedAction == null ? ActionType.None : _selectedAction.ActionType;

        private void Start()
        {
            BattleEventBus.OnSelectionChanged.AddListener(OnSelectionChanged);
            OnSelectionChanged();
        }

        private void OnSelectionChanged()
        {
            TurnOff();

            var selectedHex = SelectionManager._.SelectedHex;

            if (selectedHex == null)
                return;

            var champion = selectedHex.Champion;
            if (champion == null)
                return;

            if (champion.Team != Team.Home)
                return;

            foreach (var b in _allButtons)
            {
                b.SetVisiblity(true);
            }
        }

        private void TurnOff()
        {
            if (_selectedAction != null)
            {
                UnSelect(_selectedAction);
            }

            foreach (var b in _allButtons)
            {
                b.SetVisiblity(false);
            }
        }

        public ActionButton GetSelectedAction()
        {
            return _selectedAction;
        }

        public void Select(ActionButton actionButton)
        {
            if(_selectedAction != null)
            {
                UnSelect(_selectedAction);
            }

            _selectedAction = actionButton;
            _selectedAction.MarkSelection(true);
        }

        public void UnSelect(ActionButton actionButton)
        {
            if (_selectedAction == null)
                return;

            if(_selectedAction != actionButton)
            {
                Debug.LogWarning($"{_selectedAction} is not equal {actionButton}");
            }

            _selectedAction.MarkSelection(false);

            _selectedAction = null;
        }
    }
}