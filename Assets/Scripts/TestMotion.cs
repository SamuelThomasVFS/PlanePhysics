using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TestMotion : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float linearSpeed = 100f;

    private float _xInput = 0f;
    private float _zInput = 0f;
    private float _yInput = 0f;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        _xInput = Input.GetAxis("Horizontal");
        _zInput = Input.GetAxis("Vertical");
        _yInput = 0f;
        if (Input.GetKey("e")) _yInput = 1f;
        if (Input.GetKey("q")) _yInput = -1f;
    }
    private void FixedUpdate()
    {
        _rigidbody.AddRelativeForce(_xInput * linearSpeed, 0f, _zInput * linearSpeed);
        _rigidbody.AddTorque(0f, _yInput * rotationSpeed, 0f);
    }
}
