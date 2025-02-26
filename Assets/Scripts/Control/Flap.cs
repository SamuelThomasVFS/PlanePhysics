using System;
using UnityEngine;

public class Flap : PlaneComponent
{
    [SerializeField] private FlapType _flapType;
    [SerializeField] private Side _side;
    
    private float _flapAngleRange;

    private void Awake()
    {
        _pc = GetComponentInParent<PlaneController>();
        switch (_flapType)
        {
            case FlapType.Rudder:
                _flapAngleRange = _pc.PlaneSpecs.RudderMaxAngle;
                break;
            case FlapType.Aileron:
                _flapAngleRange = _pc.PlaneSpecs.AileronMaxAngle;
                break;
            case FlapType.Elevator:
                _flapAngleRange = _pc.PlaneSpecs.ElevatorMaxAngle;
                break;
        }
    }

    private void Update()
    {
        SetFlapAngle();
    }

    private void SetFlapAngle()
    {
        float angle = _flapAngleRange * GetFlapInput();
        if (_flapType == FlapType.Aileron && _side == Side.Left) angle *= -1;
        Quaternion rotation = transform.localRotation;
        if (_flapType == FlapType.Rudder) rotation.y = angle * Mathf.Deg2Rad;
        else rotation.x = angle * Mathf.Deg2Rad;
        transform.localRotation = rotation;
    }

    private float GetFlapInput()
    {
        switch (_flapType)
        {
            case FlapType.Rudder:
                return -_pc.YawInput;
            case FlapType.Aileron:
                return _pc.RollInput;
            case FlapType.Elevator:
                return -_pc.PitchInput;
            default:
                return 0f;
        }
    }
}

public enum FlapType
{
    Flap,
    Rudder,
    Aileron,
    Elevator
}

public enum Side
{
    Left, 
    Right
}