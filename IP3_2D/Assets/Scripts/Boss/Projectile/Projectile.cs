using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected float damage;
    protected float speed;

    public virtual void Initialize(
        float damage,
        float speed)
    {
        this.damage = damage;
        this.speed = speed;
    }
}