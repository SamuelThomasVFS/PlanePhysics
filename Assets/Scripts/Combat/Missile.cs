using TMPro.EditorUtilities;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Missile : Weapon
{
    public Transform _target;
    [SerializeField] private float _thrust;
    [SerializeField] private float _handling;
    [SerializeField] private float _maxTrackingAngle = 90;

    private bool _isTracking;
    
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlaneComponent planeComponent))
        {
            planeComponent.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(_isTracking) SeekTarget();
    }

    private void SeekTarget()
    {
        HandleTargeting();
        HandleThrust();
    }

    private void HandleThrust()
    {
        _rb.AddForce(transform.forward * _thrust);
    }

    private void HandleTargeting()
    {
        Vector3 _turnVector = Vector3.zero;

        
        
        _rb.AddTorque(_turnVector * _handling);
    }

    private void Launch(Transform seekingTarget)
    {
        _target = seekingTarget;
        _isTracking = true;
        transform.parent = null;
        _rb.isKinematic = false;
    }

    private void CheckTargetViability()
    {
        Vector3 targetVector = _target.position - transform.position;
        float angle = Vector3.Angle(transform.forward, targetVector);
        if (angle > _maxTrackingAngle)
        {
            _isTracking = false;
        }
    }
}
