using System.Threading.Tasks;
using CodeBase.Infrastructure.States;
using CodeBase.Services.LogService;
using CodeBase.Services.PlayerProgressService;
using CodeBase.Services.StaticDataService;
using CodeBase.UI.PopUps.PolicyAcceptPopup;
using CodeBase.UI.Services.PopUps;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.GameLoading.States
{
    public class PrivatePolicyState : IState
    {
        private readonly IPopUpService _popUpService;
        private readonly SceneStateMachine _sceneStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ILogService _log;
        private readonly IStaticDataService _staticData;

        public PrivatePolicyState(IPopUpService popUpService, IStaticDataService staticData, SceneStateMachine sceneStateMachine, IPersistentProgressService progressService, ILogService log)
        {
            _popUpService = popUpService;
            _sceneStateMachine = sceneStateMachine;
            _progressService = progressService;
            _log = log;
            _staticData = staticData;
        }

        public async UniTask Enter()
        {
            _log.Log("PrivatePolicyState enter");
            
            if (!_progressService.Progress.PrivatePolicyAccepted) 
                await AskToAcceptPrivatePolicy();
            
            if (_progressService.Progress.PrivatePolicyAccepted)
                _sceneStateMachine.Enter<GDPRState>().Forget();
            else
                _log.Log("Player cant play our game due to somehow reject private policy :)");
        }

        private async Task AskToAcceptPrivatePolicy()
        {
            var popupConfig = _staticData.GetPolicyAcceptPopupConfig(PolicyAcceptPopupTypes.PrivatePolicy);
            
            bool result = await _popUpService.AskPolicyPopup(popupConfig);
            
            _progressService.Progress.PrivatePolicyAccepted = result;
        }

        public UniTask Exit() => default;
    }
}