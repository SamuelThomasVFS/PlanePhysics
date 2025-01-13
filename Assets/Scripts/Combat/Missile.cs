using System.Net;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody))]
public class Missile : Weapon
{
    public Transform _target;
    [SerializeField] private float _thrust;
    [SerializeField] private float _handling;
    [SerializeField] private float _maxTrackingAngle = 90;
    [SerializeField] private float _lift;
    
    [SerializeField] private VisualEffect _exhaustParticles;

    private bool _isTracking;
    
    private Rigidbody _rb;

    [SerializeField, Header("Testing")] private bool _launchTest = false;
    [SerializeField] private Transform _testTarget;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Impact with " + other.gameObject.name);
        if (other.gameObject.TryGetComponent(out PlaneComponent planeComponent))
        {
            planeComponent.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_launchTest)
        {
            _launchTest = false;
            Launch(_testTarget);
        }
    }

    private void FixedUpdate()
    {
        if (_isTracking)
        {
            SeekTarget();
        }
    }

    private void SeekTarget()
    {
        HandleThrust();
        if (!CheckTargetViability()) return;
        HandleTurning();
    }

    private void HandleThrust()
    {
        Debug.Log("Thrust");
        _rb.AddForce(transform.forward * _thrust);
        _rb.AddForce(_rb.linearVelocity.z * transform.up * _lift);
    }

    private void HandleTurning()
    {
        if (_target == null) return;
        Vector3 _turnVector = Vector3.zero;
        
        _turnVector = Vector3.Cross(transform.forward,_target.position-transform.position);
        _rb.AddTorque(_turnVector * _handling);
    }

    public void Launch(Transform seekingTarget = null)
    {
        _target = seekingTarget;
        _isTracking = true;
        transform.parent = null;
        _rb.isKinematic = false;
    }

    private bool CheckTargetViability()
    {
        if (_target == null) return false;
        Vector3 targetVector = _target.position - transform.position;
        float angle = (Vector3.Angle(transform.forward, targetVector));
        Debug.Log(angle);
        if (angle > _maxTrackingAngle)
        {
            Debug.Log("Target is out of range");
            return false;
        }
        return true;
    }
}
