using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : PowerUp
{
    [Range(1, 10)]
    [SerializeField] private int healthToReplenish;

    protected override void ApplyEffect(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        if (playerHealth == null)
            return;

        playerHealth.Heal(healthToReplenish);
    }

    protected override void DisplayMessage()
    {
        PowerUpNotification.Instance.DisplayMessage("Health Increased");
    }

}
