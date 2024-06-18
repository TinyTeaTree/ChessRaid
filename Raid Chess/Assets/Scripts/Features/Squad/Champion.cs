using System;
using System.Threading.Tasks;
using DG.Tweening;
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
        [SerializeField] protected Animator _animator; 


        public string Id => _id;

        public Coord Location { get; private set; }
        public Direction Direction { get; private set; }
        public Team Team { get; private set; }
        public ChampionDef Def => _def;

        public int Health { get; set; }
        public int ActionPoints { get; set; }

        public void SetDirection(Direction direction)
        {
            Direction = direction;

            transform.rotation = Quaternion.Euler(GridUtils.GetEulerDirection(direction));
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

        public async Task RotateTo(Direction toDirection)
        {
            var tween = transform.DORotate(GridUtils.GetEulerDirection(toDirection), 0.5f);

            _animator.SetTrigger("Rotate");

            await TaskUtils.WaitYieldInstruction(tween.WaitForCompletion());

            SetDirection(toDirection);

            GridManager._.GetHex(Location).Champion.Direction = Direction;
        }

        public async Task MoveTo(Coord to)
        {
            Coord from = Location;
            var targetHex = GridManager._.GetHex(to);

            var tween = transform.DOMove(targetHex.transform.position, 1f);

            _animator.SetTrigger("Step");

            await TaskUtils.WaitYieldInstruction(tween.WaitForCompletion());

            GridManager._.GetHex(from).SetChampion(null);
            GridManager._.GetHex(to).SetChampion(this);

            SetLocation(to);
        }

        public virtual async Task Attack(Coord location)
        {
            _animator.SetTrigger("Attack");

            await Task.Delay(TimeSpan.FromSeconds(1f));
        }

        public async Task GetDamaged(int damage)
        {
            Health -= damage;
            _animator.SetTrigger("Damage");

            if(Health <= 0)
            {
                if(SelectionManager._.SelectedHex?.Champion == this)
                {
                    SelectionManager._.Deselect();
                }

                GridManager._.GetHex(Location).SetChampion(null);
                TurnModel._.RemoveTurnChain(this);
                Squad._.RemoveChampion(this);

                await Task.Delay(TimeSpan.FromSeconds(0.5f));

                Destroy(gameObject);
            }
        }
    }
}