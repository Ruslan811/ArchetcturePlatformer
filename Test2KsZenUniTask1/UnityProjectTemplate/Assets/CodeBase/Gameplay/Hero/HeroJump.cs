using CodeBase.Services.InputService;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HeroJump : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private HeroAnimator _animator;

    private Rigidbody _rb;
    private IInputService _inputService = new InputService();
    private bool _isGrounded;

    private void Start() => _rb = GetComponent<Rigidbody>();

    private void Update()
    {
        if (_inputService.IsJumpButtonUp() && _isGrounded)
        {
            Jump();
            Debug.Log("Jumpp");
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground ground))
            _isGrounded = true;
        else
            _isGrounded = false;
    }

    private void Jump()
    {
        _animator.PlayJump();
        _rb.AddForce(Vector3.up * _force);
        _isGrounded = false;
        
    }
}
