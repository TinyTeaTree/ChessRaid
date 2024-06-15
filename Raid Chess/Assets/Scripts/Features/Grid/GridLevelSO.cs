using System.Collections.Generic;
using UnityEngine;

namespace ChessRaid
{
    [CreateAssetMenu(fileName = "Grid Level", menuName = "Chess Raid/Level/Grid")]
    public class GridLevelSO : ScriptableObject
    {
        public List<Coord> Grid;
    }
}