using UnityEngine;

public abstract class IHealth : MonoBehaviour
{
     public abstract float MaxHealth { get; }
     public abstract float MinHealth { get; }
     public abstract float CurrentHealth { get; }


    public abstract void TakeDamage(float damage);

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
