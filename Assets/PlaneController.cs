using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AreaCalculator))]
public class PlaneController : MonoBehaviour
{
    [SerializeField] private Vector2 _input;

    [SerializeField, Header("Aircraft Specifications")] private float _dragCoefficient = 0f;

    [SerializeField, Header("Weather Conditions")] private float _fluidDensity;

    [SerializeField, Header("Testing")] private Vector3 _testVelocity;
    [SerializeField] private bool _test;
    
    private AreaCalculator _ac;
    private Rigidbody _rb;

    private void Awake()
    {
        _ac = GetComponent<AreaCalculator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_test)
        {
            _test = false;
            CalculateDrag();
        }
    }

    private void CalculateDrag()
    {
        float dragForce = _dragCoefficient * _ac.Return2DArea() * ((_fluidDensity * Mathf.Pow(ReturnAirspeed().magnitude, 2)) / 2);
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

    public void OnAccelerate(InputValue inputValue)
    {
        float accelInput = inputValue.Get<float>();
    }
}
