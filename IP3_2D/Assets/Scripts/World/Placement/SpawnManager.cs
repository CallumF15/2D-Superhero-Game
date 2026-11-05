using UnityEngine;


/// <summary>
/// Tracks player direction and spawn permission.
/// </summary>

public class SpawnManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D playerRigidbody;

    [Header("Movement Settings")]
    [SerializeField] private float movementThreshold = 0.1f;

    public SpawnDirection HorizontalDirection { get; private set; }
    public SpawnDirection VerticalDirection { get; private set; }

    public bool CanSpawn { get; private set; }

    private void Update()
    {
        UpdatePlayerDirection();
        UpdateSpawnPermission();
    }

    private void UpdatePlayerDirection()
    { 
        Vector2 velocity = playerRigidbody.linearVelocity;

        HorizontalDirection = velocity.x > 0
            ? SpawnDirection.Right
            : SpawnDirection.Left;

        VerticalDirection = velocity.y > 0
            ? SpawnDirection.Up
            : SpawnDirection.Down;

        CanSpawn =
            velocity.x > movementThreshold &&
            Mathf.Abs(velocity.y) > movementThreshold;
    }

    private void UpdateSpawnPermission()
    {
        CanSpawn = HorizontalDirection != SpawnDirection.Left && Mathf.Abs(playerRigidbody.linearVelocity.y) > movementThreshold;
    }
}
