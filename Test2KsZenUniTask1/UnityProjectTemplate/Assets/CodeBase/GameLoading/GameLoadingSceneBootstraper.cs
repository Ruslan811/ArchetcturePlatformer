using CodeBase.GameLoading.States;
using CodeBase.Infrastructure.States;
using CodeBase.Services.LogService;
using UnityEngine;
using Zenject;

namespace CodeBase.GameLoading
{
    public class GameLoadingSceneBootstraper : IInitializable
    {
        private readonly SceneStateMachine _sceneStateMachine;
        private readonly StatesFactory _statesFactory;
        private readonly ILogService _log;

        public GameLoadingSceneBootstraper(SceneStateMachine sceneStateMachine, StatesFactory statesFactory, ILogService log)
        {
            _sceneStateMachine = sceneStateMachine;
            _statesFactory = statesFactory;
            _log = log;
        }

        public void Initialize()
        {
            _log.Log("Start loading scene bootstraping");

            _sceneStateMachine.RegisterState(_statesFactory.Create<ServerConnectState>());
            _sceneStateMachine.RegisterState(_statesFactory.Create<LoadPlayerProgressState>());
            _sceneStateMachine.RegisterState(_statesFactory.Create<PrivatePolicyState>());
            _sceneStateMachine.RegisterState(_statesFactory.Create<GDPRState>());
            _sceneStateMachine.RegisterState(_statesFactory.Create<FinishGameLoadingState>());

            _log.Log("Finish loading scene bootstraping");
            
            // go to the first scene state
            _sceneStateMachine.Enter<ServerConnectState>();
        }
    }
}