using UnityEngine;

public abstract class IHealth : MonoBehaviour
{
     float MaxHealth { get; set; }
     float MinHealth { get; }
     float CurrentHealth { get; }


    public abstract void TakeDamage(float damage);

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
