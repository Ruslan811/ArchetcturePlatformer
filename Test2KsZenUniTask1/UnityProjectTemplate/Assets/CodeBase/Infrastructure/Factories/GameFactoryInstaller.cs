using Codebase.Gameplay.Hero;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.SaveLoadService;
using CodeBase.Services.StaticDataService;
using CodeBase.UI.HUD;
using Zenject;

namespace CodeBase.Infrastructure.Factories
{
    public class GameFactoryInstaller : Installer<GameFactoryInstaller>
    {
        public override void InstallBindings()
        {
            // bind sub-factories here
            Container.BindFactory<HUDRoot, HUDRoot.Factory>().FromComponentInNewPrefabResource(InfrastructureAssetPath.HUDRoot);
            Container.BindFactory<Money, Money.Factory>().FromComponentInNewPrefabResource(InfrastructureAssetPath.Money);
            //Container.BindFactory<Hero, Hero.Factory>().FromComponentInNewPrefabResource(InfrastructureAssetPath.Hero);
            Container.Bind<AssetProvider>().AsCached();
            Container.Bind<StaticDataService>().AsSingle();
            Container.Bind<SaveLoadService>().AsSingle(); 

            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
        }
    }
}