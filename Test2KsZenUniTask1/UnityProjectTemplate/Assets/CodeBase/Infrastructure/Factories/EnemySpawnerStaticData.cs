using CodeBase.Data;
using CodeBase.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData
{
    [Serializable]
    public class EnemySpawnerStaticData
    {
        public string Id;
        public EnemyTypeId MonsterTypeId;
        public Vector3 Position;

        public EnemySpawnerStaticData(string id, EnemyTypeId monsterTypeId, Vector3 position)
        {
            Id = id;
            MonsterTypeId = monsterTypeId;
            Position = position;
        }
    }
}

/*namespace CodeBase.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId MonsterTypeId;

        public string Id { get; set; }

        private IGameFactory _factory;

        private EnemyDeath _enemyDeath;

        private bool _slain;

        public void Construct(IGameFactory gameFactory) =>
          _factory = gameFactory;

        private void OnDestroy()
        {
            if (_enemyDeath != null)
                _enemyDeath.Happened -= Slay;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(Id))
                _slain = true;
            else
                Spawn();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            List<string> slainSpawnersList = progress.KillData.ClearedSpawners;

            if (_slain && !slainSpawnersList.Contains(Id))
                slainSpawnersList.Add(Id);
        }

        private async void Spawn()
        {
            GameObject monster = await _factory.CreateMonster(MonsterTypeId, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null)
                _enemyDeath.Happened -= Slay;

            _slain = true;
        }
    }
}*/