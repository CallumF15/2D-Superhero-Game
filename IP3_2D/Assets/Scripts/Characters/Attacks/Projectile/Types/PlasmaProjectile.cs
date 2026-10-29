using UnityEngine;

public class PlasmaProjectile : Projectile
{
    protected override void DealDamage(IDamageable target, Collider2D other)
    {
        if (target == null) return;

        // Immediate impact damage
        target.TakeDamage(damage);
    }
}
