using CodeBase.Infrastructure.States;
using CodeBase.Services.LogService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.GameLoading.States
{
    public class FinishGameLoadingState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ILogService _log;

        public FinishGameLoadingState(GameStateMachine gameStateMachine, ILogService log)
        {
            _gameStateMachine = gameStateMachine;
            _log = log;
        }

        public async UniTask Enter()
        {
            _log.Log("FinishGameLoadingState enter");
            
            _gameStateMachine.Enter<GameHubState>().Forget();
        }

        public UniTask Exit() => 
            default;
    }
}