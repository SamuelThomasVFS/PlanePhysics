using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaneUIHandler : MonoBehaviour
{
    [SerializeField] private Slider _throttleSlider;
    [SerializeField] private TextMeshProUGUI _speedText;
    [SerializeField] private TextMeshProUGUI _fuelText;
    
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
        _speedText.text = _controller.Speed.ToString();
    }
    
    
}
