using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : PowerUp
{
    [SerializeField] private float shieldDuration = 3.0f; //How long in seconds the shield should last before player takes damage

    protected override void ApplyEffect(GameObject player)
    {
        PlayerHealth health = player.GetComponent<PlayerHealth>();

        if (health != null)
        {
            Debug.Log($"Player health: {health}");
            health.StartCoroutine(ShieldTimer(health));
        }
    }

    protected override void DisplayMessage()
    {
        PowerUpNotification.Instance.DisplayMessage("SHIELD ACTIVATED");
    }

    private IEnumerator ShieldTimer(PlayerHealth health)
    {
        health.ActivateShield();

        yield return new WaitForSeconds(shieldDuration);

        health.DeactivateShield();
    }
}
