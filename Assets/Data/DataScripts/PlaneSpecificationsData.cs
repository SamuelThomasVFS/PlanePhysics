using UnityEngine;

[CreateAssetMenu(fileName = "PlaneSpecificationsData", menuName = "Scriptable Objects/PlaneSpecificationsData")]
public class PlaneSpecificationsData : ScriptableObject
{
    [field: SerializeField] public float DragCoefficient { get; private set; }
    [field: SerializeField] public float EjectionForce { get; private set; }
}
