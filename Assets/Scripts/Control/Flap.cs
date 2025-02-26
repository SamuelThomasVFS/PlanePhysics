using UnityEngine;

public class Flap : PlaneComponent
{
    private void Update()
    {
        Debug.Log("");
    }
    
}

public enum FlapType
{
    Flap,
    Rudder,
    Aileron,
    Elevator
}
