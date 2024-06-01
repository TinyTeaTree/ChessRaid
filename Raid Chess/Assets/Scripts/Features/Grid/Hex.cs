using UnityEngine;

namespace ChessRaid
{
    public class Hex : MonoBehaviour
    {
        [SerializeField] MeshRenderer _hexFloor;
        [SerializeField] HexDef _def;

        public Coord Location;

        public Orientation Orientation { get; private set; }

        public bool IsSelected { get; private set; }

        public Champion Champion => Orientation?.Champion;

        private ActionType _action;

        public void SetOrientation(Orientation orientation)
        {
            Orientation = orientation;
        }

        public void ToggleSelect(bool select)
        {
            IsSelected = select;

            _hexFloor.material.color = select ? _def.SelectedColor : _def.NotSelectedColor;
        }

        public void SetActionSelection(ActionType action)
        {
            if(IsSelected)
            {
                Debug.LogWarning($"Can Action Select a Selected Hex, Not Definied");
                return;
            }

            _action = action;

            switch (action)
            {
                case ActionType.None:
                    _hexFloor.material.color = _def.NotSelectedColor;
                    break;
                case ActionType.Attack:
                    _hexFloor.material.color = _def.AttackColor;
                    break;
                case ActionType.Move:
                    _hexFloor.material.color = _def.MoveColor;
                    break;
                case ActionType.Rotate:
                    _hexFloor.material.color = _def.RotateColor;
                    break;
            }
        }
    }
}