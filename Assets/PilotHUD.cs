using UnityEngine;

public class PilotHUD : MonoBehaviour
{
    [SerializeField, Header("References")] private Rigidbody _rb;
    
    [SerializeField, Header("Tuning")] private Vector2 _gBlackoutRange;

    private Vector3 _previousVelocity;
    private Vector3 _currentVelocity;
    private float _acceleration;
    
    private void Update()
    {
        _previousVelocity = _currentVelocity;
        _currentVelocity = _rb.velocity;
        _acceleration = ((_currentVelocity - _previousVelocity) / Time.deltaTime).magnitude;
    }
}
