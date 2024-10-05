using System;
using System.Threading.Tasks;
using Core;
using Services;

namespace Game
{
    public class PlayerAccount : BaseFeature, IPlayerAccount
    {
        [Inject] public IPlayerSaveService Saver { get; set; }

        [Inject] public PlayerAccountRecord Record { get; set; }

        public void CreateNewPlayer()
        {
            Record.PlayerId = System.Guid.NewGuid().ToString();
            Record.CreationDate = DateTime.UtcNow;

            Record.NickName = string.Empty;
            Record.AvatarId = AvatarId.Empty;
        }

        public Task LinkCredentials()
        {
            throw new System.NotImplementedException();
        }

        public async Task Login()
        {
            var savedJson = await Saver.GetSavedJson(Saves.PlayerAccount);
            if(savedJson == null)
            {
                CreateNewPlayer();
                await SyncPlayerData();
            }
            else
            {
                Record.Populate(savedJson);
            }

            Record.SessionId = System.Guid.NewGuid().ToString();
        }

        public Task Logout()
        {
            throw new System.NotImplementedException();
        }

        public async Task SyncPlayerData()
        {
            await Saver.SaveData(Record, Saves.PlayerAccount);
        }
    }
}