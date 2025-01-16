using TMPro;
using UnityEngine;

public class Flap : PlaneComponent
{
    [Header("Drag Calculations")]
    [field: SerializeField] public float PlanarArea { get; private set; }
    [field: SerializeField] public float DragCoefficient { get; private set; }

    [SerializeField, Header("Flap Positions")] private float _rotationRange;
    [SerializeField] private Transform _flapOrigin;
    
    [SerializeField] private float _testInput;
    
    public void ApplyForce(Rigidbody rb, float airDensity)
    {
        // Calculate magnitude of drag force
        float forceMagnitude = 0.5f * airDensity * Mathf.Pow(rb.linearVelocity.magnitude, 2) * DragCoefficient * EstimateArea(rb);
        Vector3 force = forceMagnitude * transform.up;
        

        rb.AddForceAtPosition(force, transform.position);
    }

    private void Update()
    {
        SetOrientation(_testInput);
    }

    public void SetOrientation(float input)
    {
        Quaternion rotation = _flapOrigin.rotation;
        rotation.x = input * _rotationRange;
        transform.rotation = rotation;
    }

    private float EstimateArea(Rigidbody rb)
    {
        float angle = Vector3.Angle(transform.up, rb.linearVelocity);
        float ratio = Mathf.Cos(angle);
        return PlanarArea * ratio;
    }
}
