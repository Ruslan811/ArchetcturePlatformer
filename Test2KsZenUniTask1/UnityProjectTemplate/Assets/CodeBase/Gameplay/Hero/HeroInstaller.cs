using CodeBase.Services.SaveLoadService;
using UnityEngine;
using Zenject;

public class HeroInstaller : Installer<HeroInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ISaveLoadService>().AsSingle();
        Debug.Log("ffffffhhhhhj000");
    }
}
