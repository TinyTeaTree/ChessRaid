using UnityEngine;

namespace ChessRaid
{
    public class GameManager : MonoBehaviour
    {
        void Start()
        {
            DataWarehouse._.Init();
            GridManager._.SetUp();
            Squad._.SetUp();
            TurnManager._.Start();
        }
    }
}