using UnityEngine;

[CreateAssetMenu(fileName = "FlightConditionsData", menuName = "Scriptable Objects/FlightConditionsData")]
public class FlightConditionsData : ScriptableObject
{
    [field: SerializeField] public float AirDensity { get; private set; } = 1f;
    [field: SerializeField] public float WindSpeed { get; private set; }
    [field: SerializeField] public Vector3 WindDirection { get; private set; }
}
