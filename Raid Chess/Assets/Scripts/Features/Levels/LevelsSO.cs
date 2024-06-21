using System.Collections.Generic;
using UnityEngine;

namespace ChessRaid
{
    [CreateAssetMenu(fileName = "Levels SO", menuName = "Chess Raid/Definitions/Levelss")]
    public class LevelsSO : ScriptableObject
    {
        public List<GridLevelSO> Levels;
    }
}