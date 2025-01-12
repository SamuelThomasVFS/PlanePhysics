using UnityEngine;

public abstract class PlaneComponent : MonoBehaviour
{
    public float Health { get; set; } = 100f;

    [SerializeField] private ParticleSystem _disabledParticleSystem;
    
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }
    
    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            DisableComponent();    
        }
    }

    private void DisableComponent()
    {
        _collider.enabled = false;
    }
}
