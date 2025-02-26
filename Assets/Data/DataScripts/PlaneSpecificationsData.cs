using UnityEngine;

[CreateAssetMenu(fileName = "PlaneSpecificationsData", menuName = "Scriptable Objects/PlaneSpecificationsData")]
public class PlaneSpecificationsData : ScriptableObject
{
    [field: SerializeField] public float DragCoefficient { get; private set; }
    [field: SerializeField] public float EjectionForce { get; private set; }
    [field: SerializeField] public Vector2 ThrottleRange { get; private set; } = new Vector2(0f, 1f);
    [field: SerializeField] public Vector2 RudderAngleRange { get; private set; } = new Vector2(-45f, 45f);
    [field: SerializeField] public Vector2 AileronAngleRange { get; private set; } = new Vector2(-45f, 45f);
    [field: SerializeField] public Vector2 ElevatorAngleRange { get; private set; } = new Vector2(-45f, 45f);
    
    [field: SerializeField] public float ThrottleInputAcceleration { get; private set; } = 0.5f;
    [field: SerializeField] public float YawInputAcceleration { get; private set; } = 0.5f;
    [field: SerializeField] public float RollInputAcceleration { get; private set; } = 0.5f;
    [field: SerializeField] public float PitchInputAcceleration { get; private set; } = 0.5f;
}
