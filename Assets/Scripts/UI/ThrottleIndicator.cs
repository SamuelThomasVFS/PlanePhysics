using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ThrottleIndicator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlaneController _planeController;
    
    private Vector2 _throttleRange;
    private Slider _slider;
    private TextMeshProUGUI _text;
    
    private void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        if (_planeController) _throttleRange = _planeController.PlaneSpecs.ThrottleRange;
        _slider.minValue = _throttleRange.x;
        _slider.maxValue = _throttleRange.y;
    }
    
    private void Update()
    {
        float throttle = _planeController.Throttle;
        _text.text = throttle.ToString().Truncate(4, "");
        _slider.value = throttle;
    }
}