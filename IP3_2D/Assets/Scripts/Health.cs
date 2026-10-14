using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 100f;
    protected float currentHealth;

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
    }


}