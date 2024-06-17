using Codebase.Gameplay.Hero;
using CodeBase.Services.SaveLoadService;
using CodeBase.StaticData;
using CodeBase.UI.HUD;
using CodeBase.UI.Money;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
    public interface IGameFactory
    {
        /*List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }*/

        IHUDRoot CreateHUD();
        IMoney CreateMoney();
        // IHero CreateHero();
        public Cysharp.Threading.Tasks.UniTask<GameObject> CreateHero(Vector3 at);
        void Cleanup();
        UniTask<GameObject> CreateEnemy(EnemyTypeId typeId, Transform parent);
        UniTask CreateSpawner(string spawnerId, Vector3 at, EnemyTypeId monsterTypeId);
    }
}