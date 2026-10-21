using UnityEngine;

public class SpeedPowerUp : PowerUp
{
    [SerializeField] private float speedIncrease = 5f;

    protected override void ApplyEffect(GameObject player)
    {
        PlayerMovement movement = player.GetComponent<PlayerMovement>();

        if (movement != null)
        {
            // Implement a speed increase method
            //movement.IncreaseSpeed(speedIncrease);
        }
    }

    protected override void DisplayMessage()
    {
        PowerUpNotification.Instance.DisplayMessage("Speed Boost Activated!");
    }
}