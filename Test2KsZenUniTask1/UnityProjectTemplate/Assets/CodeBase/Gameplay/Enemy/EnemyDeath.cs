using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class  EnemyDeath : MonoBehaviour
    {
        public EnemyHealth Health;

        public event Action Happened;

        private void Start()
        {
            Health.HealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            Health.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (Health.Current <= 0)
                Die();
        }

        private void Die()
        {
            Health.HealthChanged -= OnHealthChanged;

            /*Animator.PlayDeath();
            SpawnDeathFx();*/

            StartCoroutine(DestroyTimer());

            Happened?.Invoke();
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
            Debug.Log("EnemyDeathhh");
        }
    }
}