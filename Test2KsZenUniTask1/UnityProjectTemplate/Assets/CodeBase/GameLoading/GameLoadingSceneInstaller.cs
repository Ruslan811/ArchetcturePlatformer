using CodeBase.GameLoading.States;
using CodeBase.Infrastructure.States;
using CodeBase.UI;
using CodeBase.UI.PopUps.PolicyAcceptPopup;
using UnityEngine;
using Zenject;

namespace CodeBase.GameLoading
{
    public class GameLoadingSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("Start loading scene installer");
            
            Container.BindInterfacesAndSelfTo<GameLoadingSceneBootstraper>().AsSingle().NonLazy(); // non lazy due to it's not injected anywhere but we still need to instanciate it

            Container.BindInterfacesAndSelfTo<StatesFactory>().AsSingle();

            Container.Bind<SceneStateMachine>().AsSingle();
            
            UIInstaller.Install(Container);
        }
    }
}