using UnityEngine;

public class Thruster : PlaneComponent
{
    [SerializeField] private float _power = 5f;

    private float _thrust = 0f;

    private void Update()
    {
        GetThrust();
    }

    private void FixedUpdate()
    {
        FireThruster();
    }
    
    private void GetThrust()
    {
        _thrust = _pc.Throttle * _power;
    }
    
    private void FireThruster()
    {
        _rb.AddForceAtPosition(transform.forward * _thrust, transform.position);
    }
}
