using UnityEngine;

namespace ChessRaid
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] Grid _grid;
        [SerializeField] Squad _squad;

        void Start()
        {
            _grid.SetUp();
            _squad.SetUp();
        }
    }
}