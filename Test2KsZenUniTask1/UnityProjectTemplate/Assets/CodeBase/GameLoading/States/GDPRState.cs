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
    public class GDPRState : IState
    {
        private readonly IPopUpService _popUpService;
        private readonly SceneStateMachine _sceneStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ILogService _log;
        private readonly IStaticDataService _staticDataService;

        public GDPRState(IPopUpService popUpService, IStaticDataService staticDataService, SceneStateMachine sceneStateMachine, IPersistentProgressService progressService, ILogService log)
        {
            _popUpService = popUpService;
            _sceneStateMachine = sceneStateMachine;
            _progressService = progressService;
            _log = log;
            _staticDataService = staticDataService;
        }

        public async UniTask Enter()
        {
            _log.Log("GDPRState enter");

            if(!_progressService.Progress.GDPRPolicyAccepted)
                await AskToAcceptGDPRPolicy();
            
            if (_progressService.Progress.GDPRPolicyAccepted)
                _sceneStateMachine.Enter<FinishGameLoadingState>().Forget();
            else
                _log.Log("Player cant play our game due to reject gdpr policy :)");
        }

        private async Task AskToAcceptGDPRPolicy()
        {
            var popupConfig = _staticDataService.GetPolicyAcceptPopupConfig(PolicyAcceptPopupTypes.GDPR);
            
            bool result = await _popUpService.AskPolicyPopup(popupConfig);

            _progressService.Progress.GDPRPolicyAccepted = result;
        }

        public UniTask Exit() => default;
    }
}