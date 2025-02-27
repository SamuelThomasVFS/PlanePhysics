using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlaneUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _speedText;
    [SerializeField] private TextMeshProUGUI _fuelText;
    
    private PlaneController _controller;

    private void Awake()
    {
        _controller = GetComponentInParent<PlaneController>();
    }

    private void Update()
    {
        _speedText.text = FilterSpeed(_controller.Speed);
        
    }

    private string FilterSpeed(float speed)
    {
        string speedText = speed.ToString();
        speedText = speedText.Truncate(3, "m/s");
        return speedText;
    }
    
}
