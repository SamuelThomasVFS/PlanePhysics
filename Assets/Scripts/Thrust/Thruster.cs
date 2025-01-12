using UnityEngine;

public class Thruster : PlaneComponent
{
    [SerializeField] private float _power;

    public void FireThruster(Rigidbody rb, float throttle)
    {
        if (Health <= 0) return;
        float thrust = throttle * _power;
        rb.AddForceAtPosition(transform.forward * thrust, transform.position);
    }
}
