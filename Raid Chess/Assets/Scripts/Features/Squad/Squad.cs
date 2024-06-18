using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ChessRaid
{
    public class Squad : WagMonoton<Squad>
    {
        [SerializeField] List<ChampionDef> _championDefinitions;

        List<Champion> _champions;

        public IEnumerable<Champion> GetChampions(Team team)
        {
            foreach(var c in _champions)
            {
                if(c.Team == team)
                {
                    yield return c;
                }
            }
        }

        public void SetUp()
        {
            _champions = new List<Champion>();

            var startingState = GridManager._.StartingState;

            foreach (var loc in startingState.Board)
            {
                var championPrefab = _championDefinitions.First(c => c.Id == loc.ChampionId).Prefab;
                var hex = GridManager._.GetHex(loc.Location);
                var championInstance = Instantiate(championPrefab, hex.transform);

                _champions.Add(championInstance);

                hex.SetChampion(championInstance);

                championInstance.SetLocation(loc.Location);
                championInstance.SetDirection(loc.Direction);
                championInstance.SetTeam(loc.Team);
                championInstance.Health = championInstance.Def.Stats.Health;
                championInstance.ActionPoints = championInstance.Def.Stats.Speed * 10;
            }
        }

        public void RemoveChampion(Champion champion)
        {
            _champions.RemoveAt(_champions.FindIndex(o => o == champion));
        }

    }
}