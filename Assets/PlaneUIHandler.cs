using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlaneUIHandler : MonoBehaviour
{
    [SerializeField] private Slider _throttleSlider;
    [SerializeField] private TextMeshProUGUI _speedText;
    [SerializeField] private TextMeshProUGUI _fuelText;
    [SerializeField] private RectTransform _compassImage;
    
    private PlaneController _controller;

    private void Awake()
    {
        _controller = GetComponentInParent<PlaneController>();

        _throttleSlider.minValue = _controller.PlaneSpecs.ThrottleRange.x;
        _throttleSlider.maxValue = _controller.PlaneSpecs.ThrottleRange.y;
    }

    private void Update()
    {
        _throttleSlider.value = _controller.Throttle;
        _speedText.text = FilterSpeed(_controller.Speed);
        HandleCompass();
    }

    private string FilterSpeed(float speed)
    {
        string speedText = speed.ToString();
        speedText = speedText.Truncate(3, "m/s");
        return speedText;
    }

    private void HandleCompass()
    {
        Vector3 forward = transform.forward;
        forward.y = 0f;
        float angle = Vector3.SignedAngle(Vector3.forward, forward, Vector3.up);
        Vector3 compassRotation = new Vector3(0f, 0f, angle);
        _compassImage.rotation = Quaternion.Euler(compassRotation);
    }
    
    
}
