using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField] private RectTransform _compassRotator;
    [SerializeField] private Transform _observedTransform;
    [SerializeField] private Vector3 _northDirection;

    private void Update()
    {
        HandleCompass();
    }
    
    private void HandleCompass()
    {
        Vector3 forward = _observedTransform.forward;
        forward.y = 0f;
        float angle = Vector3.SignedAngle(_northDirection, forward, Vector3.up);
        Vector3 compassRotation = new Vector3(0f, 0f, angle);
        _compassRotator.rotation = Quaternion.Euler(compassRotation);
    }
}
