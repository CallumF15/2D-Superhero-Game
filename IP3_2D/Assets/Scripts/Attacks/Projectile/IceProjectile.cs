using System;
using UnityEngine;

public class IceProjectile : Projectile
{
    [Header("Ice Damage")]
    [SerializeField] private float iceDamage = 0.01f;   //damage of the burn 
    [SerializeField] private float iceDuration = 3f; //how long the slow effect will last
    [Range(0.01f,0.6f)]
    [SerializeField] private float slowPercentage = 0.09f; //how much to slow the player
    [SerializeField] private float iceStacks = 0f; //use later to impact speed

    protected override void TakeDamage(GameObject target, float damage, float speed)
    {
        IHealth health = target.GetComponent<IHealth>();

        if (health == null) return;

        // Immediate impact damage
        health.TakeDamage(damage);

        ApplyIce(target);
    }

    private void ApplyIce(GameObject target)
    {
        StatusEffectManager manager = target.GetComponent<StatusEffectManager>();

        if (manager == null) return;

        Debug.Log("Applying Ice Effect");
        manager.ApplyEffect(new IceEffect(manager, slowPercentage, iceDuration));
    }
}
