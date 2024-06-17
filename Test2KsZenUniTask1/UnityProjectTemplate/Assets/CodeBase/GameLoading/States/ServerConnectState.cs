using CodeBase.Infrastructure.States;
using CodeBase.Services.LogService;
using CodeBase.Services.ServerConnectionService;
using CodeBase.Services.StaticDataService;
using CodeBase.UI.Overlays;
using CodeBase.UI.Services.PopUps;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.GameLoading.States
{
    public class ServerConnectState : IState
    {
        private readonly IServerConnectionService _serverConnectionService;
        private readonly IStaticDataService _staticDataService;
        private readonly SceneStateMachine _sceneStateMachine;
        private readonly IAwaitingOverlay _awaitingOverlay;
        private readonly IPopUpService _popUpService;
        private readonly ILogService _log;

        public ServerConnectState(IServerConnectionService serverConnectionService, IStaticDataService staticDataService, SceneStateMachine sceneStateMachine, IAwaitingOverlay awaitingOverlay, IPopUpService popUpService, ILogService log)
        {
            _serverConnectionService = serverConnectionService;
            _staticDataService = staticDataService;
            _sceneStateMachine = sceneStateMachine;
            _awaitingOverlay = awaitingOverlay;
            _popUpService = popUpService;
            _log = log;
        }

        public async UniTask Enter()
        {
            _log.Log("ServerConnectState enter");
            _awaitingOverlay.Show("Connection to server...");
            
            ConnectionResult result = await _serverConnectionService.Connect(_staticDataService.ServerConnectionConfig);
            
            _awaitingOverlay.Hide();
            
            if(result == ConnectionResult.Success)
                _sceneStateMachine.Enter<LoadPlayerProgressState>().Forget();
            else
            {
                // some works on connection error for example repeat
                await _popUpService.ShowError("Connection error",
                    "Can't connect to server. Please check your internet connection.");
            }
        }

        public UniTask Exit() => default;
    }
}