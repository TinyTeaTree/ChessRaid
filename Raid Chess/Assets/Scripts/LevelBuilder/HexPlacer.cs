using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChessRaid.LevelBuilder
{
    public class HexPlacer : MonoBehaviour
    {
        [SerializeField] Hex _hexOriginal;
        [SerializeField] Transform _startPoint;
        [SerializeField] float _heightOffset = 0.05f;

        [SerializeField] GridLevelSO _so;

        private Dictionary<Coord, Hex> _placedHexes;

        public void Start()
        {
            _hexOriginal.gameObject.SetActive(false);
            PlaceFirstHex();
        }

        [ContextMenu("Save")]
        public void Save()
        {
            _so.Grid = new List<Coord>();
            foreach(var kvp in _placedHexes)
            {
                if (kvp.Value.gameObject.activeSelf)
                {
                    _so.Grid.Add(kvp.Key);
                }
            }
        }


        [ContextMenu("Start")]
        public void PlaceFirstHex()
        {
            _placedHexes = new();

            if (Physics.Raycast(new Ray(_startPoint.position, Vector3.down), out var hit, 100))
            {
                for(int x = -50; x<=50; ++x)
                {
                    for(int y = -50; y<=50; ++y)
                    {
                        var newHex = Instantiate(_hexOriginal, transform);

                        newHex.transform.position = new Vector3(
                            x * Consts.NextHexXDistance,
                            0f,
                            y * Consts.NextHexYDistance + (x % 2 != 0 ? Consts.OddHexYOffset : 0f)
                        );

                        newHex.gameObject.SetActive(false);

                        if (Physics.Raycast(new Ray(newHex.transform.position + new Vector3(0, 100f, 0), Vector3.down), out var hexHit, 200))
                        {
                            newHex.transform.position = hexHit.point + new Vector3(0f, _heightOffset, 0f);
                        }

                        _placedHexes[new Coord(x, y)] = newHex;

                        newHex.name = $"Hex [{x},{y}]";
                    }
                }
            }

            foreach(var p in _so.Grid)
            {
                _placedHexes[p].gameObject.SetActive(true);
            }
        }

        public void Place(Ray mouseRay)
        {
            if (Physics.Raycast(mouseRay, out var hit, 1000))
            {
                var hitPoint = hit.point;

                var x = hitPoint.x;
                var xCoord = Mathf.RoundToInt(x / Consts.NextHexXDistance);

                var y = hitPoint.z;
                var yCoord = Mathf.RoundToInt((y - (xCoord % 2 != 0 ? Consts.OddHexYOffset : 0f)) / Consts.NextHexYDistance);

                Coord coord = new Coord(xCoord, yCoord);

                var hex = _placedHexes[coord];

                hex.gameObject.SetActive(!hex.gameObject.activeSelf);
            }
        }
    }
}