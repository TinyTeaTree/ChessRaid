using Agents;
using Core;
using Factories;
using Services;

namespace Game
{
    public class GameBootstrap : GameInfra
    {
        protected override void AddServices()
        {
            var Notebook = new NotebookService();
            Core.Notebook.NotebookService = Notebook;

            _services.Add<INotebookService>(Notebook);
            _services.Add<ISummoningService>(new SummoningService());
            _services.Add<ILocalConfigService>(new LocalConfigService());
            _services.Add<ISoundPlayerService>(new SoundPlayerService());
            _services.Add<IPlayerSaveService>(new PlayerSaveService());
            _services.Add<IRecordService>(new RecordService());
            //<New Service>
        }

        protected override void AddFeatures()
        {
            _features.Add<ILoadingScreen>(new LoadingScreen());
            _features.Add<IHud>(new Hud());
            _features.Add<ILobby>(new Lobby());
            _features.Add<IArena>(new Arena());
            _features.Add<IGrid>(new Grid());
            _features.Add<IMobs>(new Mobs());
            _features.Add<IPlayerAccount>(new PlayerAccount());
            //<New Feature>
        }

        protected override void AddFactories()
        {
            _factories.Add(typeof(HudVisual), new ResourceFactory(Addresses.HudVisual));
            _factories.Add(typeof(LoadingScreenVisual), new ResourceFactory(Addresses.LoadingScreen));
        }

        protected override void AddAgents()
        {
            _agents.Add<IAppLaunchAgent>(new AppLaunchAgent());
            _agents.Add<IAppExitAgent>(new AppExitAgent());
            //<New Agent>
        }

        protected override void AddRecords()
        {
            _records.Add(typeof(LobbyRecord), new LobbyRecord());
            _records.Add(typeof(GridRecord), new GridRecord());
            _records.Add(typeof(MobsRecord), new MobsRecord());
            _records.Add(typeof(LoadingScreenRecord), new LoadingScreenRecord());
            _records.Add(typeof(PlayerAccountRecord), new PlayerAccountRecord());
            //<New Record>
        }

        protected override void BootstrapCustoms()
        {
            BootstrapRecordService();
            BootstrapSoundPlayerService();
            BootstrapSummoningService();
            BootstrapLocalConfigurationService();

            AppExitAgent.SelfRegister(_agents.Get<IAppExitAgent>());
        }

        private void BootstrapRecordService()
        {
            var recordService = _services.Get<IRecordService>();
            recordService.SetUp(_records.Values);
        }

        private void BootstrapSoundPlayerService()
        {
            var soundPlayer = _services.Get<ISoundPlayerService>();
            DJ.SoundPlayer = soundPlayer;
        }

        private void BootstrapLocalConfigurationService()
        {
            var localConfigService = _services.Get<ILocalConfigService>();
            localConfigService.SetConfigSO(Services.Get<ISummoningService>().LoadResource<LocalConfigCollectionSO>(Addresses.LocalConfigs));
        }

        private void BootstrapSummoningService()
        {
            var summoner = _services.Get<ISummoningService>();
            Summoner.SummoningService = summoner;
        }

        protected override void StartGame()
        {
            new GameLaunchFlow(this).Execute();
        }
    }
}
