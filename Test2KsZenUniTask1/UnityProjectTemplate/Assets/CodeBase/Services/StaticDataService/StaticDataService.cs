using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.LogService;
using CodeBase.Services.ServerConnectionService;
using CodeBase.StaticData;
using CodeBase.UI.PopUps.PolicyAcceptPopup;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase.Services.StaticDataService
{
    // This service incapsulate logic of uploading configs and give convenient API
    // for all consumers to receive necessary configs
    public class StaticDataService : IStaticDataService
    {
        public ServerConnectionConfig ServerConnectionConfig { get; private set; }
        private readonly ILogService log;
        private IAssetProvider assetProvider;
        private Dictionary<int, PolicyAcceptPopupConfig> policyAcceptConfigs;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<EnemyTypeId, EnemyStaticData> _monsters;
        private const string LevelsDataPath = "StaticData/Levels/GamePlay1";
        private const string EnemyDataPath = "StaticData/Enemies/Evrei";

        public StaticDataService(ILogService log, IAssetProvider assetProvider)
        {
            this.log = log;
            this.assetProvider = assetProvider;
        }

        public async UniTask InitializeAsync()
        {
            // load your configs here
            List<UniTask> tasks = new List<UniTask>();
            tasks.Add(LoadServerConfigs());
            tasks.Add(LoadPolicyAcceptConfigs());

            _levels = Resources
        .LoadAll<LevelStaticData>(LevelsDataPath)
        .ToDictionary(x => x.LevelKey, x => x);
            await UniTask.WhenAll(tasks);
            log.Log("Static data loaded");

            _monsters = Resources
       .LoadAll<EnemyStaticData>(EnemyDataPath)
       .ToDictionary(x => x.EnemyTypeId, x => x);
        }

        public LevelStaticData ForLevel(string sceneKey) =>
      _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
        ? staticData
        : null;

        public EnemyStaticData ForMonster(EnemyTypeId typeId) =>
      _monsters.TryGetValue(typeId, out EnemyStaticData staticData)
        ? staticData
        : null;

        private async UniTask LoadPolicyAcceptConfigs()
        {
            var configs = await GetConfigs<PolicyAcceptPopupConfig>();
            policyAcceptConfigs = configs.ToDictionary(config => (int)config.Type, config => config);
        }

        private async UniTask LoadServerConfigs()
        {
            var configs = await GetConfigs<ServerConnectionConfig>();
            if(configs.Length > 0)
                ServerConnectionConfig = configs.First();
            else
                log.LogError("There are no server connection config founded!");
        }

        private async UniTask<List<string>> GetConfigKeys<TConfig>() => 
            await assetProvider.GetAssetsListByLabel<TConfig>(AssetLabels.Configs);

        private async UniTask<TConfig[]> GetConfigs<TConfig>() where TConfig : class
        {
            var keys = await GetConfigKeys<TConfig>();
            return await assetProvider.LoadAll<TConfig>(keys);
        }

        public PolicyAcceptPopupConfig GetPolicyAcceptPopupConfig(PolicyAcceptPopupTypes type) =>
            policyAcceptConfigs[(int)type];
    }
}