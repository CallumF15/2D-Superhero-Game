using UnityEngine;

public class SpawnPositionProvider : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Camera mainCamera;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnDistanceX = 5f;
    [SerializeField] private float verticalDirectionOffset = 3f;
    [SerializeField] private float randomYRange = 2f;
    [SerializeField] private float offsetZ = 0f;

    [Header("World Bounds")]
    [SerializeField] private float minimumHeight = -10f;
    [SerializeField] private float maximumHeight = 10f;
    [SerializeField] private float edgePadding = 1f;

    public Vector3 GetSpawnPosition(SpawnDirection horizontalDirection, SpawnDirection verticalDirection)
    {
        float distance = Mathf.Abs(player.position.z - mainCamera.transform.position.z);

        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f, distance));

        Vector3 topRight = mainCamera.ViewportToWorldPoint(
            new Vector3(1f, 1f, distance)
        );

        float minY = Mathf.Max(bottomLeft.y, minimumHeight);
        float maxY = Mathf.Min(topRight.y, maximumHeight);

        float baseY = player.position.y;

        if (horizontalDirection == SpawnDirection.Right &&
            verticalDirection == SpawnDirection.Up)
        {
            baseY += verticalDirectionOffset;
        }
        else if (horizontalDirection == SpawnDirection.Right &&
                 verticalDirection == SpawnDirection.Down)
        {
            baseY -= verticalDirectionOffset;
        }

        float randomY = Random.Range(-randomYRange, randomYRange);

        float spawnY = Mathf.Clamp(
            baseY + randomY,
            minY + edgePadding,
            maxY - edgePadding
        );

        float spawnX = topRight.x + spawnDistanceX;

        return new Vector3(
            spawnX,
            spawnY,
            player.position.z + offsetZ
        );
    }
}