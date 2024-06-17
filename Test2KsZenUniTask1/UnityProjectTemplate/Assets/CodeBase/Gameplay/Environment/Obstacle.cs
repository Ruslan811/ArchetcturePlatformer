using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out HeroHealth hero))
            hero.TakeDamage(_damage);
    }
}
