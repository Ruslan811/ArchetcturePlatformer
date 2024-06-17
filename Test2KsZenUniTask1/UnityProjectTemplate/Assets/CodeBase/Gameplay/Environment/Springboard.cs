using UnityEngine;

namespace CodeBase.Gameplay.Environment
{
    public class Springboard : MonoBehaviour
    {
        [SerializeField] private float _force;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out HeroJump hero))
                collision.rigidbody.AddForce(Vector3.up * _force, ForceMode.Impulse);
        }
    }
}
