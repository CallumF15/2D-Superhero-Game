using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireProjectile : Projectile
{
    [SerializeField] private float burnDamage = 0.01f;   //damage of the burn 
    [SerializeField] private float burnDuration = 3f; //how long the burn will damage
    [SerializeField] private float burnInterval = 1f; //

    protected override void TakeDamage(GameObject target)
    {
        IHealth health = target.GetComponent<IHealth>();
        //need to change to Abstraction? Player/boss health abstraction interface

        if (health == null) return;

        // Immediate impact damage
        health.TakeDamage(damage);

        ApplyBurn(target);
    }

    private void ApplyBurn(GameObject target)
    {
        StatusEffectManager manager = target.GetComponent<StatusEffectManager>();

        if (manager == null) return;

        manager.ApplyEffect(new BurnEffect(manager, burnDamage, burnInterval, burnDuration));
    }
}
