using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.SceneManagement;
using CodeBase.Infrastructure.UI.LoadingCurtain;
using CodeBase.Services.LogService;
using CodeBase.Services.PlayerProgressService;
using CodeBase.Services.SaveLoadService;
using CodeBase.Services.StaticDataService;
using CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class GameplayState : IState
    {
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly ISceneLoader _sceneLoader;
        private ILogService _log;
        private IAssetProvider _assetProvider;
        private IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoad;
        [Inject] private readonly IGameFactory gameFactory;

        public GameplayState(ILoadingCurtain loadingCurtain, ISceneLoader sceneLoader, ILogService log, IAssetProvider assetProvider, StaticDataService staticDataService, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _loadingCurtain = loadingCurtain;
            _sceneLoader = sceneLoader;
            _log = log;
            _assetProvider = assetProvider;
            _staticData = staticDataService;
            _progressService = progressService;
            _saveLoad = saveLoadService;
        }

        public async UniTask Enter()
        {
            _log.Log("Game mode 1 state enter");
            _loadingCurtain.Show();

            await _assetProvider.WarmupAssetsByLabel(AssetLabels.GameplayState);
            await _sceneLoader.Load(InfrastructureAssetPath.GameMode1Scene);

            _loadingCurtain.Hide();

            LevelStaticData levelData = LevelStaticData();
            GameObject hero = await gameFactory.CreateHero(levelData.InitialHeroPosition);

            _saveLoad.SaveProgress();
            await InitSpawners(levelData);
            
        }


        private async UniTask InitSpawners(LevelStaticData levelStaticData)
        {
            foreach (EnemySpawnerStaticData spawnerData in levelStaticData.EnemySpawners)
                await gameFactory.CreateSpawner(spawnerData.Id, spawnerData.Position, spawnerData.MonsterTypeId);
        }

        private LevelStaticData LevelStaticData() =>
     _staticData.ForLevel(SceneManager.GetActiveScene().name);

        public async UniTask Exit()
        {
            _loadingCurtain.Show();
            await _assetProvider.ReleaseAssetsByLabel(AssetLabels.GameplayState);
        }
    }
}