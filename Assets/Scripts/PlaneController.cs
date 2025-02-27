using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AreaCalculator))]
public class PlaneController : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private FlightConditionsData _flightConditions;
    [field: SerializeField] public PlaneSpecificationsData PlaneSpecs;
    
    [field: SerializeField] public float Throttle { get; private set; }
    [field: SerializeField] public float YawInput { get; private set; }
    [field: SerializeField] public float RollInput { get; private set; }
    [field: SerializeField] public float PitchInput { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    
    private List<PlaneComponent> _components = new List<PlaneComponent>();
    private List<Thruster> _thrusters = new List<Thruster>();
    
    // Essential components
    private PlayerInput _playerInput;
    private Rigidbody _rb;
    private AreaCalculator _ac;
    
    // Input variables
    private float _yawInput;
    private float _accelInput;
    private Vector2 _mouseInput;

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        HandleInput();
        
        // Set Throttle
        Throttle += _accelInput * PlaneSpecs.ThrottleInputAcceleration * Time.deltaTime;
        Throttle = Mathf.Clamp(Throttle, PlaneSpecs.ThrottleRange.x, PlaneSpecs.ThrottleRange.y);
        
        // Set publicly accessible speed
        Speed = _rb.linearVelocity.magnitude;

    }

    private void FixedUpdate()
    {
        // Todo: Handle drag using advanced system, current is placeholder
        Vector3 drag = -_rb.linearVelocity * PlaneSpecs.DragCoefficient;
        _rb.AddForce(drag, ForceMode.Force);
    }

    private void Initialize()
    {
        // Initialize essential references
        _playerInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody>();
        _ac = GetComponent<AreaCalculator>();
        
        // Get a list of all components
        _components = GetComponentsInChildren<PlaneComponent>().ToList();

        // Divide the list into specific lists and provide key references
        for (int i = 0; i < _components.Count; i++)
        {
            _components[i].SetParentComponents(_rb, _flightConditions, this);
            if (_components[i].TryGetComponent(out Thruster thruster)) _thrusters.Add(thruster);
        }
    }

    private Vector3 GetAirspeed()
    {
        return -_rb.linearVelocity;
    }

    // -- Input handler methods --
    private void HandleInput()
    {
        _mouseInput.x = Input.GetAxis("Mouse X");
        _mouseInput.y = Input.GetAxis("Mouse Y");
    }
    
    public void OnMove(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();
        _yawInput = input.x;
        _accelInput = input.y;
    }

    public void OnFire1(InputValue inputValue)
    {
        Debug.Log("Fire1");
    }

    public void OnFire2(InputValue inputValue)
    {
        Debug.Log("Fire2");
    }
}
