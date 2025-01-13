using TMPro;
using UnityEngine;

public class Flap : MonoBehaviour
{
    [field: SerializeField] public float PlanarArea { get; private set; }
    [field: SerializeField] public float DragCoefficient { get; private set; }

    public void ApplyForce(Rigidbody rb, float airDensity)
    {
        // Calculate magnitude of drag force
        float forceMagnitude = 0.5f * airDensity * Mathf.Pow(rb.velocity.magnitude, 2) * DragCoefficient * EstimateArea(rb);
        Vector3 force = forceMagnitude * transform.up;
        

        rb.AddForceAtPosition(force, transform.position);
    }

    private float EstimateArea(Rigidbody rb)
    {
        float angle = Vector3.Angle(transform.up, rb.velocity);
        float ratio = Mathf.Cos(angle);
        return PlanarArea * ratio;
    }
}
