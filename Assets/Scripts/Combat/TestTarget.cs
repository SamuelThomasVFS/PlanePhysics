using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TestTarget : PlaneComponent
{
    private void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
