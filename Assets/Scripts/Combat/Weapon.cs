using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float Damage { get; private set; }

    protected void HitTarget(PlaneComponent pc)
    {
        pc.TakeDamage(Damage);
    }
}
