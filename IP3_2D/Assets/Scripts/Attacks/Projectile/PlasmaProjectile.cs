using UnityEngine;

public class PlasmaProjectile : Projectile
{
    protected override void TakeDamage(GameObject target, float damage, float speed)
    {
        //slows the player/boss movement with some damage
        IHealth health = target.GetComponent<IHealth>();
        //need to change to Abstraction? Player/boss health abstraction interface

        if (health == null) return;

        // Immediate impact damage
        health.TakeDamage(damage);

    }
}
