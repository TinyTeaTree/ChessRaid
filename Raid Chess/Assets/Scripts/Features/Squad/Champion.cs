using UnityEngine;

namespace ChessRaid
{
    public class Champion : MonoBehaviour
    {
        [SerializeField] string _id;
        [SerializeField] ChampionDef _def;
        [SerializeField] MeshRenderer _teamRenderer;
        [SerializeField] Color _homeColor;
        [SerializeField] Color _awayColor;

        public string Id => _id;

        public Coord Location { get; private set; }
        public Direction Dir { get; private set; }
        public Team Team { get; private set; }

        public void SetDirection(Direction dir)
        {
            Dir = dir;

            transform.rotation = Quaternion.Euler(0, (int)Dir * 360f / 6f, 0);
        }

        public void SetLocation(Coord location)
        {
            Location = location;
        }

        public void SetTeam(Team team)
        {
            Team = team;

            if(Team == Team.Home)
            {
                _teamRenderer.material.color = _homeColor;
            }
            else
            {
                _teamRenderer.material.color = _awayColor;
            }
        }
    }
}