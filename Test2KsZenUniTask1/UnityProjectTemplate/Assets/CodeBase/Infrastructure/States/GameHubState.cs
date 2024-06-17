using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.SceneManagement;
using CodeBase.Infrastructure.UI.LoadingCurtain;
using CodeBase.Services.LogService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class GameHubState : IState
    {
        private readonly ILoadingCurtain loadingCurtain;
        private readonly ISceneLoader sceneLoader;
        private readonly ILogService log;
        private readonly IAssetProvider assetProvider;
        private readonly GameStateMachine gameStateMachine;

        public GameHubState(ILoadingCurtain loadingCurtain, ISceneLoader sceneLoader, ILogService log, IAssetProvider assetProvider, GameStateMachine gameStateMachine)
        {
            this.loadingCurtain = loadingCurtain;
            this.sceneLoader = sceneLoader;
            this.log = log;
            this.assetProvider = assetProvider;
            this.gameStateMachine = gameStateMachine;
        }

        public async UniTask Enter()
        {
            log.Log("GameHub state exter");
            loadingCurtain.Show();

            await assetProvider.WarmupAssetsByLabel(AssetLabels.GameHubState);
           // await assetProvider.WarmupAssetsByLabel(AssetLabels.LoadProgressState);
            // due to we don't have any substates for this state jet we just load scene with game hub decorations
            await sceneLoader.Load(InfrastructureAssetPath.GameHubScene);
            
            loadingCurtain.Hide();
        }

        public async UniTask Exit()
        {
            loadingCurtain.Show();
            await assetProvider.ReleaseAssetsByLabel(AssetLabels.GameHubState);
            //gameStateMachine.Enter<LoadProgressState>().Forget();
        }
    }
}