using System;
using System.Threading.Tasks;
using Agents;
using Core;

namespace Game
{
    public class GameLaunchFlow : SequenceFlow
    {
        public GameLaunchFlow(IBootstrap bootstrap)
        {
            this.AddNext(action: () => bootstrap.Agents.Get<IAppLaunchAgent>().AppLaunch())
                .AddNext(action: () => bootstrap.Features.Get<ILoadingScreen>().Show(LoadingScreenType.Start))
                .AddNext(asyncMethod: () => Task.Delay(TimeSpan.FromSeconds(1f))) // Pretend to do something
                .AddParallel(asyncMethod: Create()
                    .AddNext(asyncMethod: bootstrap.Features.Get<IPlayerAccount>().Login)
                    .ExecuteAsync
                )
                //.AddNext(asyncMethod: () => bootstrap.Features.Get<ILobby>().OpenLobby())
                //.AddNext(action: () => bootstrap.Features.Get<ILoadingScreen>().HideLoadingScreen())
                ;
        }
    }
}