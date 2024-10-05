using Core;

namespace Game
{
    public class Lobby : BaseVisualFeature<LobbyVisual>, ILobby
    {
        [Inject] public LobbyRecord Record { get; set; }
    }
}