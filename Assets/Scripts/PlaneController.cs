using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AreaCalculator))]
public class PlaneController : MonoBehaviour
{
    [field:SerializeField] private Transform LockedTarget { get; set; }
    
    // Input
    private Vector2 _input;

    // Preferences
    [SerializeField, Header("Aircraft Specifications")] private float _dragCoefficient = 0f;
    [SerializeField] private List<Thruster> _thrusters = new List<Thruster>();
    [SerializeField] private List<Flap> _flaps = new List<Flap>();

    [SerializeField, Header("Weapon Specifications")] private Transform _missileBay; 
    [SerializeField] private GameObject _missilePrefab;
    
    
    [SerializeField, Header("Weather Conditions")] private float _airDensity;

    [SerializeField, Header("Testing")] private Vector3 _testVelocity;
    [SerializeField] private bool _test;
    
    private AreaCalculator _ac;
    private Rigidbody _rb;

    private void Awake()
    {
        _ac = GetComponent<AreaCalculator>();
        _rb = GetComponent<Rigidbody>();
        _thrusters.Clear();
        _thrusters = GetComponentsInChildren<Thruster>(true).ToList();
        _flaps.Clear();
        _flaps = GetComponentsInChildren<Flap>(true).ToList();
    }

    private void Update()
    {
        if (_test)
        {
            _test = false;
            CalculateDrag();
        }
    }

    private void FixedUpdate()
    {
        FireThrusters();
    }

    private void FireThrusters()
    {
        for (int i = 0; i < _thrusters.Count; i++)
        {
            _thrusters[i].FireThruster(_rb, _input.y);
        }
    }

    private void CheckFlaps()
    {
        for (int i = 0; i < _flaps.Count; i++)
        {
            _flaps[i].ApplyForce(_rb, _airDensity);
        }
    }

    private void CalculateDrag()
    {
        float dragForce = _dragCoefficient * _ac.Return2DArea() * ((_airDensity * Mathf.Pow(ReturnAirspeed().magnitude, 2)) / 2);
        Vector3 dragVector = dragForce * ReturnAirspeed().normalized;
        Debug.Log(dragVector + ", " + dragForce);
    }

    private Vector3 ReturnAirspeed()
    {
        if (_testVelocity != Vector3.zero) return _testVelocity;
        return -_rb.linearVelocity;
    }

    public void OnMove(InputValue inputValue)
    {
        _input = inputValue.Get<Vector2>();
        Debug.Log(_input);
    }

    public void OnFire1(InputValue inputValue)
    {
        Debug.Log("Shooting");
    }

    public void OnFire2(InputValue inputValue)
    {
        Debug.Log("Missile away");
        if (Instantiate(_missilePrefab, _missileBay.position, _missileBay.rotation).TryGetComponent(out Missile missile) && LockedTarget != null)
        {
            missile.Launch(LockedTarget);
        }
    }
}
