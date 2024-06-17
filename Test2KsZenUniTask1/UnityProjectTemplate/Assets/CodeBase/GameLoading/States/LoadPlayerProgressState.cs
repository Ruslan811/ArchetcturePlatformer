using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using CodeBase.Services.LogService;
using CodeBase.Services.PlayerProgressService;
using CodeBase.Services.SaveLoadService;
using CodeBase.Services.WalletService;
using CodeBase.UI.Overlays;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.GameLoading.States
{
    public class LoadPlayerProgressState : IState
    {
        private readonly SceneStateMachine _sceneStateMachine;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IEnumerable<IProgressReader> _progressReaderServices;
        private readonly IPersistentProgressService _progressService;
        private readonly IAwaitingOverlay _awaitingOverlay;
        private readonly ILogService _log;

        public LoadPlayerProgressState(SceneStateMachine sceneStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService, IEnumerable<IProgressReader> progressReaderServices, IAwaitingOverlay awaitingOverlay, ILogService log)
        {
            _sceneStateMachine = sceneStateMachine;
            _saveLoadService = saveLoadService;
            _progressService = progressService;
            _progressReaderServices = progressReaderServices;
            _awaitingOverlay = awaitingOverlay;
            _log = log;
        }

        public async UniTask Enter()
        {
            _log.Log("LoadPlayerProgressState enter");
            
            _awaitingOverlay.Show("Loading player progress...");

            var progress = LoadProgressOrInitNew();
            _saveLoadService.SaveProgress();
            NotifyProgressReaderServices(progress);

            await UniTask.WaitForSeconds(1f); 
            _awaitingOverlay.Hide();
            
            _sceneStateMachine.Enter<PrivatePolicyState>().Forget();
           
        }

       

        private void NotifyProgressReaderServices(PlayerProgress progress)
        {
            foreach (var reader in _progressReaderServices)
                reader.LoadProgress(progress);
        }

        public UniTask Exit()
        {
            _log.Log("LoadPlayerProgressState exit");
            return default;
        }

        private PlayerProgress LoadProgressOrInitNew()
        {
            _progressService.Progress = 
                _saveLoadService.LoadProgress() 
                ?? NewProgress();
            return _progressService.Progress;
        }

        private PlayerProgress NewProgress()
        {
            var progress =  new PlayerProgress(InfrastructureAssetPath.GameLoadingScene);

            Debug.Log("Init new player progress");
            
            progress.HeroState.MaxHP = 50;
           
            progress.HeroState.ResetHP();
            Debug.Log(progress.HeroState.CurrentHP);
            progress.PrivatePolicyAccepted = false;
            progress.GDPRPolicyAccepted = false;
            progress.WalletsData = new WalletsData(new Dictionary<int, long>());
            
            return progress;
        }
    }
}