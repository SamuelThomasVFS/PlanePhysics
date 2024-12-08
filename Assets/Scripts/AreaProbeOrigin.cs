using UnityEngine;


public class AreaProbeOrigin : MonoBehaviour
{
    [SerializeField] private Transform _parentAircraft;
    [SerializeField] private float _offsetMagnitude;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = _parentAircraft.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Debug.DrawRay(_parentAircraft.position, _rigidbody.linearVelocity, Color.magenta);
        if (_rigidbody == null) return;
        if (_rigidbody.linearVelocity.magnitude == 0f) return;
        Vector3 velocity = _rigidbody.linearVelocity.normalized;
        transform.position = _parentAircraft.position + _rigidbody.linearVelocity.normalized * _offsetMagnitude;
        transform.forward = -_rigidbody.linearVelocity.normalized;
    }
}
