using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float minHealth = 0f;
    [SerializeField] private float currentHealth = 100f;

    [Header("UI")]
    [SerializeField] private Image healthBar;

    public bool IsShielded { get; private set; }

    public override float MaxHealth => maxHealth;
    public override float MinHealth => minHealth;
    public override float CurrentHealth => currentHealth;

    private void Start()
    {
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);

        Healthbar.HealthBarImage = healthBar;
        Healthbar.SetHealthBarValue(currentHealth);
    }

    public override void TakeDamage(float damage)
    {
        if (IsShielded || damage <= 0f)
            return;

        currentHealth = Mathf.Clamp(
            currentHealth - damage,
            minHealth,
            maxHealth
        );

        Healthbar.SetHealthBarValue(currentHealth);

        if (currentHealth <= minHealth)
        {
            Debug.Log("Player has died.");
            Die();
            GameManager.Instance.PlayerDied();
        }
    }

    public void Heal(int amount)
    {
        if (amount <= 0 || currentHealth <= minHealth)
            return;

        currentHealth = Mathf.Clamp(
            currentHealth + amount,
            minHealth,
            maxHealth
        );

        Healthbar.SetHealthBarValue(currentHealth);
    }

    public void ActivateShield()
    {
        IsShielded = true;
    }

    public void DeactivateShield()
    {
        IsShielded = false;
    }
}