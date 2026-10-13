using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
	[Range(0.5f, 1)]
	[SerializeField] private float health = 1; // Player's health value, ranging from 0.5 to 1

    [SerializeField] private Image healthBar; // Reference to the health bar image in the UI

    public float Health
    {
        get => health;
    }

    // Use this for initialization
    void Start () 
	{
		Healthbar.HealthBarImage = healthBar; 
        Healthbar.SetHealthBarValue(health); // Initialize the health bar with the player's health value
    }

	public void TakeDamage(float damage)
	{
		health -= damage; // Reduce the player's health by the specified damage amount

        if (health <= 0)
        {
            // Handle player death (e.g., display game over screen, restart level, etc.)
            Debug.Log("Player has died.");
        }

        Healthbar.SetHealthBarValue(health); // Update the health bar to reflect the new health value		
    }
	
	/// <summary>
	/// If player is hit by enemy's attack. Reduce health and destroy attack game object
	/// </summary>
	/// <param name="other"></param>
	//void OnCollisionEnter(Collision other)
	//{
	//	if (other.gameObject.tag == "bossAttack") { 
		
			
	//		Destroy(other.gameObject);

	//		if (Healthbar.GetHealthBarValue() == 0)
 //           {
 //               //Destroy(gameObject);
 //               //Display game over screen
 //           }
 //       }
	//}
}
