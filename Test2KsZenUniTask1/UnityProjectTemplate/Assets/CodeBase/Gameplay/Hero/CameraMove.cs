using UnityEngine;
using Zenject.SpaceFighter;

[RequireComponent(typeof(Camera))]
public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothTime = 5.0f;
    [SerializeField] private Vector3 _offset = new Vector3(0, 2, -5);

    void FixedUpdate() => Move();

    private void Move()
    {
        Vector3 nextPosition = transform.position = Vector3.Lerp(transform.position, _target.position + _offset, Time.deltaTime * _smoothTime);
        transform.position = nextPosition;
    }
}