using UnityEngine;

[CreateAssetMenu(fileName = "PlaneSpecificationsData", menuName = "Scriptable Objects/PlaneSpecificationsData")]
public class PlaneSpecificationsData : ScriptableObject
{
    [field: SerializeField] public float DragCoefficient { get; private set; }
    [field: SerializeField] public float EjectionForce { get; private set; }
    [field: SerializeField] public Vector2 ThrottleRange { get; private set; } = new Vector2(0f, 1f);
    [field: SerializeField] public float ThrottleInputAcceleration { get; private set; } = 0.1f;
}
