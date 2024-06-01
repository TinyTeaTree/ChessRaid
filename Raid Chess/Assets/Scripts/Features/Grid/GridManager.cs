using System;
using System.Collections.Generic;
using UnityEngine;


namespace ChessRaid
{
    public class GridManager : WagMonoton<GridManager>
    {
        [SerializeField] Hex _hexPrefab;
        [SerializeField] Transform _hexRoot;

        [SerializeField] List<Hex> _allHexes;

        Dictionary<Coord, Hex> _hexMap;

        public void SetUp()
        {
            _hexMap = new Dictionary<Coord, Hex>();

            CreateHexes();

            MapHexes();
        }

        public Hex GetHex(Coord coord)
        {
            return _hexMap[coord];
        }

        void MapHexes()
        {
            foreach (var h in _allHexes)
            {
                _hexMap.Add(h.Location, h);
            }
        }

        void CreateHexes()
        {
            if (_allHexes.IsNullOrEmpty())
            {
                for (int x = 0; x < 10; ++x)
                {
                    for (int y = 0; y < 10; ++y)
                    {
                        var hex = Instantiate(_hexPrefab, _hexRoot);

                        hex.Location = (x, y);

                        hex.transform.position = new Vector3(x * Consts.NextHexXDistance, 0f, y * Consts.NextHexYDistance + (x % 2 == 1 ? Consts.OddHexYOffset : 0f));

                        hex.name = $"Hex [{x},{y}]";

                        _allHexes.Add(hex);
                    }
                }
            }
        }

        public void MarkHexAction(Hex hitHex, ActionType selectedAction)
        {
            hitHex.SetActionSelection(selectedAction);
        }
    }
}