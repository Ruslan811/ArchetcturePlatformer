using UnityEngine;

public class HeroDeath : MonoBehaviour
{
    [SerializeField] private HeroHealth _health;
    private bool _isDead;

    private void Start() => 
        _health.HealthChanged += HealthChanged;

    private void OnDestroy() => 
        _health.HealthChanged -= HealthChanged;

    private void HealthChanged()
    {
        if (!_isDead && _health.Current <= 0)
            Die();
    }

    private void Die()
    {
        _isDead = true;
        Debug.Log("Died");
    }
}
