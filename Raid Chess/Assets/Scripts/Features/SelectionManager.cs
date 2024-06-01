using UnityEngine;
using UnityEngine.EventSystems;

namespace ChessRaid
{
    public class SelectionManager : WagMonoton<SelectionManager>
    {
        [SerializeField] Camera _camera;

        Hex _selectedHex;

        public Hex SelectedHex => _selectedHex;

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                var mouseRay = _camera.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(mouseRay,out var hit, 1000, LayerMask.GetMask("Hex")))
                {
                    var hitHex = hit.transform.parent.parent.GetComponent<Hex>();
                    ManageHexClick(hitHex);
                }
                else
                {
                    Deselect();
                }
            }
        }

        private void ManageHexClick(Hex hitHex)
        {
            var selectedAction = ActionPanel._.SelectedAction;

            if (selectedAction == ActionType.None)
            {
                ManageHexSelection(hitHex);
            }
            else if(hitHex == _selectedHex)
            {
                ManageHexSelection(hitHex);
            }
            else
            {
                Grid._.MarkHexAction(hitHex, selectedAction);   
            }
        }

        private void ManageHexSelection(Hex hitHex)
        {
            if (hitHex.IsSelected)
            {
                Deselect();
            }
            else
            {
                Select(hitHex);
            }
        }

        public void Select(Hex hex)
        {
            if(_selectedHex != null)
            {
                DeselectInternal(false);
            }

            _selectedHex = hex;
            _selectedHex.ToggleSelect(true);

            BattleEventBus.OnSelectionChanged.Invoke();
        }

        public void Deselect()
        {
            DeselectInternal(true);
        }

        private void DeselectInternal(bool shouldInvoke)
        {
            if (_selectedHex == null)
                return;

            _selectedHex.ToggleSelect(false);
            _selectedHex = null;

            if(shouldInvoke)
            { 
                BattleEventBus.OnSelectionChanged.Invoke();
            }
        }
    }
}