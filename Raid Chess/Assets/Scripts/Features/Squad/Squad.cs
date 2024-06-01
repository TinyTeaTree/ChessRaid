using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ChessRaid
{
    public class Squad : WagMonoton<Squad>
    {
        [SerializeField] List<ChampionDef> _championDefinitions;

        [SerializeField] GridState _startingState;

        List<Orientation> _champions;

        public void SetUp()
        {
            _champions = new List<Orientation>();

            foreach (var loc in _startingState.Board)
            {
                var championPrefab = _championDefinitions.First(c => c.Id == loc.ChampionId).Prefab;
                var hex = Grid._.GetHex(loc.Location);
                var championInstance = Instantiate(championPrefab, hex.transform);
                Orientation orientation = new Orientation()
                {
                    Champion = championInstance,
                    Location = loc.Location,
                    Team = loc.Team,
                    Direction = loc.Direction
                };
                _champions.Add(orientation);

                hex.SetOrientation(orientation);

                championInstance.SetLocation(loc.Location);
                championInstance.SetDirection(loc.Direction);
                championInstance.SetTeam(loc.Team);
            }
        }

    }
}