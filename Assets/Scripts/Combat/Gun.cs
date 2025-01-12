using System;
using UnityEngine;

public class Gun : Weapon
{
    private void Fire()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit) && hit.transform.TryGetComponent(out PlaneComponent planeComponent))
        {
            planeComponent.TakeDamage(Damage);
        }
    }
}
