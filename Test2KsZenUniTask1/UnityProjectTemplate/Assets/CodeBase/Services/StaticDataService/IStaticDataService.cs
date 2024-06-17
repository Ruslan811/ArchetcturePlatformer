using CodeBase.Services.ServerConnectionService;
using CodeBase.StaticData;
using CodeBase.UI.PopUps.PolicyAcceptPopup;
using Cysharp.Threading.Tasks;

namespace CodeBase.Services.StaticDataService
{
    public interface IStaticDataService
    {
        UniTask InitializeAsync();
        ServerConnectionConfig ServerConnectionConfig { get; }
        PolicyAcceptPopupConfig GetPolicyAcceptPopupConfig(PolicyAcceptPopupTypes type);
        LevelStaticData ForLevel(string sceneKey);
        EnemyStaticData ForMonster(EnemyTypeId typeId);
    }
}