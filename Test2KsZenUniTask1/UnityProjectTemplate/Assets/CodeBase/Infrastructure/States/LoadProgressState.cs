using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.PlayerProgressService;
using CodeBase.Services.SaveLoadService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadProgress;
        //private readonly ISceneLoader sceneLoader;
        
        private readonly IAssetProvider assetProvider;

        public LoadProgressState(IPersistentProgressService progressService, ISaveLoadService saveLoadProgress, IAssetProvider assetProvider)
        {
            _progressService = progressService;
            _saveLoadProgress = saveLoadProgress;
            this.assetProvider = assetProvider;
            
        }

        public async UniTask Enter()
        {
            LoadProgressOrInitNew();
            await assetProvider.WarmupAssetsByLabel(AssetLabels.LoadProgressState);
            //await sceneLoader.Load(InfrastructureAssetPath.)
        }

        public async UniTask Exit()
        {
            await assetProvider.ReleaseAssetsByLabel(AssetLabels.LoadProgressState);
            Debug.Log("897789787978897897");
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress =
              _saveLoadProgress.LoadProgress()
              ?? NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress(InfrastructureAssetPath.GameMode1Scene);
            progress.HeroState.MaxHP = 50;
            progress.HeroState.ResetHP();
            Debug.Log(progress.WorldData.PositionOnLevel.Position);
            return progress;
        }
    }
}