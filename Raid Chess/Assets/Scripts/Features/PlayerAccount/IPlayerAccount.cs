using System.Threading.Tasks;
using Core;

namespace Game
{
    public interface IPlayerAccount : IFeature
    {
        Task Login();
        Task Logout();

        Task LinkCredentials();

        void CreateNewPlayer();
        Task SyncPlayerData();
    }
}