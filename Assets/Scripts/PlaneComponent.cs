using UnityEngine;

public abstract class PlaneComponent : MonoBehaviour
{
    public float Health { get; private set; } = 100f;

    [SerializeField] private ParticleSystem _disabledParticleSystem;
    
    private Collider _collider;
    protected Rigidbody _rb;
    protected FlightConditionsData _fcd;
    protected PlaneController _pc;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _pc = GetComponentInParent<PlaneController>();
    }
    
    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            DisableComponent();    
        }
    }

    public void SetParentComponents(Rigidbody rb, FlightConditionsData fcd, PlaneController pc)
    {
        _rb = rb;
        _fcd = fcd;
        _pc = pc;
    }

    private void DisableComponent()
    {
        _collider.enabled = false;
    }
}

