using UnityEngine;
using UnityEngine.UI;

public class BossHealth : Health
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float minHealth = 0f;
    [SerializeField] private Slider healthSlider;
    [SerializeField] BossStateManager bossStateManager;

    private float currentHealth;

    public override float MaxHealth => maxHealth;
    public override float MinHealth => minHealth;
    public override float CurrentHealth => currentHealth;


    private void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.minValue = minHealth;
            healthSlider.value = currentHealth;
        }
    }

    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);

        bossStateManager.UpdatePhase(currentHealth, maxHealth);

        if (healthSlider != null)
            healthSlider.value = currentHealth;

        if (currentHealth <= minHealth)
            Die();
    }
}