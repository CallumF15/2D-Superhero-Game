using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : IHealth
{
    [SerializeField] private Image healthBar; // Reference to the health bar image in the UI

    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected float minHealth = 0f;

    [Range(0.5f, 1)]
    [SerializeField] protected float currentHealth = 1f; // Player's health value, ranging from 0.5 to 1

    public bool IsShielded { get; private set; }

    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public float MinHealth
    {
        get => minHealth;
        set
        {
            minHealth = Mathf.Min(value, maxHealth);
            currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
        }
    }

    public float CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = Mathf.Clamp(value, minHealth, maxHealth);
    }

    // Use this for initialization
    void Start () 
	{
		Healthbar.HealthBarImage = healthBar; 
        Healthbar.SetHealthBarValue(CurrentHealth); // Initialize the health bar with the player's health value
    }

	public override void TakeDamage(float damage)
	{
        if (IsShielded) //player has shield powerup (don't damage)
            return;

        CurrentHealth -= damage; // Reduce the player's health by the specified damage amount

        if (CurrentHealth <= 0)
        {
            // Handle player death (e.g., display game over screen, restart level, etc.)
            Debug.Log("Player has died.");
            Die();
        }

        Healthbar.SetHealthBarValue(CurrentHealth); // Update the health bar to reflect the new health value		
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
