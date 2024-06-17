using CodeBase;
using CodeBase.Services.InputService;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class HeroMove : MonoBehaviour
{
    //[SerializeField] private CharacterController _characterController;
    [SerializeField] private HeroAnimator _animator;
    [SerializeField] private float _movementSpeed;

    private IInputService _inputService = new InputService();
    private Rigidbody _rb;
    private Camera _camera;

    private void Awake()
    {
        //_inputService = AllServices.Container.Single<IInputService>();
    }

    private void Start()
    {
        _camera = Camera.main;
        _rb = GetComponent<Rigidbody>();
    }

    [Inject]
    void Construct(IInputService inputService)
    {
        _inputService = inputService;
    }

    private void FixedUpdate()
    {
        /*if (_inputService == null)
            Debug.Log("null");*/
        /* Vector3 movementVector = Vector3.zero;

          if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
          {
              movementVector = _camera.transform.TransformDirection(_inputService.Axis);
             // movementVector.y = 0;
              movementVector.Normalize();

             // transform.forward = movementVector;
          }

          if (_characterController.isGrounded)
              movementVector.y += 100;
          movementVector += Physics.gravity;

          _characterController.Move(_movementSpeed * movementVector * Time.deltaTime);
          */
        Move(_inputService.Axis);
    }

    private void Move(Vector3 direction)
    {
        _animator.PlayRun(direction.x);
        Vector3 offset = direction * (_movementSpeed * Time.deltaTime);
        _rb.MovePosition(_rb.position + offset);
        
        //_rb.AddForce(offset);
        /*if (Input.GetKeyDown(KeyCode.D))
            _animator.PlayRun(_movementSpeed);
       /* else
            _animator.ResetToIdle();*/
    }
}
