using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireProjectile : Projectile
{
    [Header("Burn Damage")]
    [SerializeField] private float burnDamage = 0.01f;  //instant damage of the burn 
    [SerializeField] private float burnDuration = 3f; //how long the burn will damage
    [SerializeField] private float burnInterval = 1f; //how often the burn damage should occur within duration

    ///DealDamage(other.gameObject, this.damage, this.speed);
    
    protected override void DealDamage(IDamageable target, Collider2D targetCollider)
    {
        if (target == null) return;

        // Immediate impact damage
        target.TakeDamage(damage);

        ApplyBurn(targetCollider);
    }

    private void ApplyBurn(Collider2D targetCollider)
    {
        StatusEffectManager manager = targetCollider.GetComponent<StatusEffectManager>();

        if (manager == null) return;

        manager.ApplyEffect(new BurnEffect(manager, burnDamage, burnInterval, burnDuration));
    }
}
