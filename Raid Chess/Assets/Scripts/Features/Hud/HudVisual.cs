using Core;
using UnityEngine;

namespace Game
{
    public class HudVisual : BaseVisual<Hud>
    {
        [SerializeField] private Camera _hudCamera;

        public Camera HudCamera => _hudCamera;

        [SerializeField] private Transform _hudRoot;
        public Transform HudRoot => _hudRoot;
    }
}