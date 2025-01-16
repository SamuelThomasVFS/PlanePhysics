using System;
using NUnit.Framework;
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
    private Transform _lockedTarget;
    
    // Weapons
    [SerializeField, Header("Weapon Specifications")] private Transform _missileBay; 
    [SerializeField] private GameObject _missilePrefab;
    
    // Plane modules
    [SerializeField, Header("Modules")] private List<Thruster> _thrusters = new List<Thruster>();
    private List<Flap> _flaps = new List<Flap>();
    [SerializeField] private List<Flap> _elevators = new List<Flap>();
    [SerializeField] private List<Flap> _ailerons = new List<Flap>();
    [SerializeField] private List<Flap> _rudders = new List<Flap>();

    [SerializeField] private Transform _pilotTransform;
    [SerializeField] private Transform _pilotSeat;
    [SerializeField] private Transform _camCenter;
    [SerializeField] private CinemachineCamera _forwardCamera;
    [SerializeField] private CinemachineCamera _firstPersonCamera;
    
    // Data
    [SerializeField, Header("Data")] private FlightConditionsData _flightConditions;
    [SerializeField] private PlaneSpecificationsData _planeSpecs;
    
    // Essential components
    private AreaCalculator _ac;
    private Rigidbody _rb;
    
    // Input
    private float _yawInput;
    private float _accelInput;
    private Vector2 _mouseInput;

    private void Awake()
    {
        _ac = GetComponent<AreaCalculator>();
        _rb = GetComponent<Rigidbody>();
        _thrusters.Clear();
        _thrusters = GetComponentsInChildren<Thruster>(true).ToList();
        foreach (var _elevator in _elevators)
        {
            _flaps.Add(_elevator);
        }
        foreach (var _aileron in _ailerons)
        {
            _flaps.Add(_aileron);
        }
        foreach (var _rudder in _rudders)
        {
            _flaps.Add(_rudder);   
        }
        _forwardCamera.Priority = 2;
    }

    private void Update()
    {
        HandleMouseInput();
    }
    
    private void FixedUpdate()
    {
        FireThrusters();
        //ApplyFlapResistance
    }

    private void FireThrusters()
    {
        for (int i = 0; i < _thrusters.Count; i++)
        {
            _thrusters[i].FireThruster(_rb, _accelInput);
        }
    }

    private void ApplyFlapResistance()
    {
        for (int i = 0; i < _flaps.Count; i++)
        {
            _flaps[i].ApplyForce(_rb, _flightConditions.AirDensity);
        }
    }

    private void CalculateDrag()
    {
        float dragForce = _planeSpecs.DragCoefficient * _ac.Return2DArea() * ((_flightConditions.AirDensity * Mathf.Pow(ReturnAirspeed().magnitude, 2)) / 2);
        Vector3 dragVector = dragForce * ReturnAirspeed().normalized;
        Debug.Log(dragVector + ", " + dragForce);
    }

    private Vector3 ReturnAirspeed()
    {
        return -_rb.linearVelocity;
    }

    // -- Input handler methods --
    private void HandleMouseInput()
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
        
    }

    public void OnFire2(InputValue inputValue)
    {
        if (Instantiate(_missilePrefab, _missileBay.position, _missileBay.rotation).TryGetComponent(out Missile missile) && _lockedTarget != null)
        {
            missile.Launch(_lockedTarget);
        }
    }

    public void OnEject(InputValue inputValue)
    {
        Debug.Log("Ejecting");
        _pilotTransform.parent = null;
        if (_pilotTransform.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(_pilotTransform.up * _planeSpecs.EjectionForce, ForceMode.Impulse);
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
