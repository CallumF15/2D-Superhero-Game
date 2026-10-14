using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public Slider healthSlider;


    void Start()
    {
        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        healthSlider.value = currentHealth;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Boss collided with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("playerAttack"))
        {
            Debug.Log("Player attack hit boss!");
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

