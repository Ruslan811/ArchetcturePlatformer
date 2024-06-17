using CodeBase.Data;
using CodeBase.Infrastructure.Factories;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AgentMoveToPlayer : Follow
    {
        private NavMeshAgent _agent;

        private const float MinimalDistance = 1;

        private IGameFactory _gameFactory;
        [SerializeField] private Transform _heroTransform;

        public void Construct(Transform heroTransform) => 
            _heroTransform = heroTransform;

        private void Start() => _agent = GetComponent<NavMeshAgent>();

        private void Update()
        {
            if(_heroTransform && IsHeroNotReached())
                _agent.destination = _heroTransform.position;
        }
    
        private bool IsHeroNotReached() => 
            _agent.transform.position.SqrMagnitudeTo(_heroTransform.position) >= MinimalDistance;
  }
}