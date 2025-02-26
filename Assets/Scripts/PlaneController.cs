using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Cinemachine;
using UnityEngine.Serialization;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AreaCalculator))]
public class PlaneController : MonoBehaviour
{
    [Header("Spatial")]
    [SerializeField] private Transform _pilotTransform;
    [SerializeField] private Transform _pilotSeat;
    [SerializeField] private Transform _camCenter;
    [SerializeField] private CinemachineCamera _forwardCamera;
    [SerializeField] private CinemachineCamera _firstPersonCamera;
    
    [Header("Data")]
    [SerializeField] private FlightConditionsData _flightConditions;
    [field: SerializeField] public PlaneSpecificationsData PlaneSpecs;
    
    [field:SerializeField] public float Throttle { get; private set; }
    [field:SerializeField] public float Speed { get; private set; }
    
    private List<PlaneComponent> _components = new List<PlaneComponent>();
    private List<Thruster> _thrusters = new List<Thruster>();
    
    // Essential components
    private PlayerInput _playerInput;
    private Rigidbody _rb;
    private AreaCalculator _ac;
    
    // Input
    private float _yawInput;
    private float _accelInput;
    private Vector2 _mouseInput;

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        GetMouseInput();
        
        // Set Throttle
        Throttle += _accelInput * PlaneSpecs.ThrottleInputAcceleration * Time.deltaTime;
        Throttle = Mathf.Clamp(Throttle, PlaneSpecs.ThrottleRange.x, PlaneSpecs.ThrottleRange.y);

        // Set public speed
        Speed = _rb.velocity.magnitude;



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

    private void CalculateDrag()
    {
        float dragForce = PlaneSpecs.DragCoefficient * _ac.Return2DArea() * ((_flightConditions.AirDensity * Mathf.Pow(GetAirspeed().magnitude, 2)) / 2);
        Vector3 dragVector = dragForce * GetAirspeed().normalized;
        Debug.Log(dragVector + ", " + dragForce);
    }

    private Vector3 GetAirspeed()
    {
        return -_rb.linearVelocity;
    }

    // -- Input handler methods --
    private void GetMouseInput()
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

    public void OnEject(InputValue inputValue)
    {
        Debug.Log("Ejecting");
        _pilotTransform.parent = null;
        if (_pilotTransform.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(_pilotTransform.up * PlaneSpecs.EjectionForce, ForceMode.Impulse);
        }
    }

    public void OnSwitchCam(InputValue inputValue)
    {
        if (_forwardCamera.Priority == 2)
        {
            _firstPersonCamera.transform.rotation = _camCenter.rotation;
            _forwardCamera.Priority = 0;
        }
        else
        {
            _firstPersonCamera.transform.rotation = _camCenter.rotation;
            _forwardCamera.Priority = 2;
        }
    }
}
