﻿using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Factories;
using CodeBase.Services.PlayerProgressService;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
  public class SpawnPoint : MonoBehaviour, IProgressSaver
  {
    public EnemyTypeId MonsterTypeId;
    
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

        private void Start()
        {
            Spawn();
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
      
      if(_slain && !slainSpawnersList.Contains(Id))
        slainSpawnersList.Add(Id);
    }

    private async void Spawn()
    {
      GameObject monster = await _factory.CreateEnemy(MonsterTypeId, transform);
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
}