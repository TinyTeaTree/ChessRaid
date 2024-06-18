using System;
using UnityEngine;
using UnityEngine.UI;

namespace ChessRaid
{
    public class CancelPanel : WagMonoton<CancelPanel>
    {
        [SerializeField] private Button _cancelOption;
        [SerializeField] private Button _undoOption;

        private void Start()
        {
            _cancelOption.onClick.AddListener(OnCancelOptionClicked);
            _undoOption.onClick.AddListener(OnUndoOptionClicked);

            BattleEventBus.OnSelectionChanged.AddListener(OnSelectionChanged);

            TurnOff();
        }

        private void OnSelectionChanged()
        {
            TurnOff();

            if (SelectionManager._.SelectedHex?.Champion != null && 
                SelectionManager._.SelectedHex?.Champion.Team == Team.Home)
            {
                TurnOn();
            }
        }

        private void OnUndoOptionClicked()
        {
            TurnModel._.UndoLastTurn();
        }

        private void OnCancelOptionClicked()
        {
            TurnModel._.RemoveTurnChain(SelectionManager._.SelectedHex?.Champion);
        }

        private void TurnOn() 
        {
            _cancelOption.gameObject.SetActive(true);
            _undoOption.gameObject.SetActive(true);
        }

        private void TurnOff()
        {
            _cancelOption.gameObject.SetActive(false);
            _undoOption.gameObject.SetActive(false);
        }
    }
}