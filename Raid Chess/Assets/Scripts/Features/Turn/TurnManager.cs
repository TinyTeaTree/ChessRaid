using UnityEngine;

namespace ChessRaid
{
    public class TurnManager : WagSingleton<TurnManager>
    {
        private TurnBox _box;

        public void Start()
        {
            _box = DataWarehouse._.GetBox<TurnBox>();
        }
    }
}