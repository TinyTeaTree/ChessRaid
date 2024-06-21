using UnityEngine;

namespace ChessRaid
{
    public class MobsManager : WagSingleton<MobsManager>
    {
        private MobState _state;
        private MobsView _view;

        public override void Awake(ContextGroup<IController> group)
        {
            GameObject go = new GameObject("Mobs");
            _view = go.AddComponent<MobsView>();
        }

        public override void Start()
        {
            _state = PlayerManager.Single.State.Grid.Mobs;
        }
    }
}