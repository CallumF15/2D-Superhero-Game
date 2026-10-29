
using UnityEngine;
using System.Collections.Generic;

public class BackgroundSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private List<GameObject> worldObjectList;

    [Header("Initial Spawning")]
    [SerializeField] private bool spawnAtStart = true;
    [SerializeField] private int initialSpawnCount = 5;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnDistanceX = 2f;
    [SerializeField] private float spawnDistanceY = 2f;
    [SerializeField] private float offsetZ = 0f;
    [SerializeField] private float delay = 1f;

    [Header("World Boundaries")]
    [SerializeField] private float maximumHeight = 1500f;
    [SerializeField] private float minimumHeight = 0f;

    [Header("Movement Detection")]
    [SerializeField] private float movementThreshold = 0.001f;

    [Header("Anti-Cheating / Lingering")]
    [SerializeField] private float maxLingerTime = 5f;
    [SerializeField] private float lingerMovementDistance = 3f;

    // Spawn timer
    private float lastSpawnTime;

    // Previous player position
    private float prevPosX;
    private float prevPosY;

    // Furthest position travelled
    private float furthestPlayerX;

    // Anti-lingering
    private float lingerTimer;
    private Vector3 lingerStartPosition;
    private bool playerLingering;

    // Prevent consecutive identical prefab selections
    private int lastIndex = -1;

    // Camera boundaries
    private float screenLeft;
    private float screenRight;
    private float screenTop;
    private float screenBottom;


    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (player == null || mainCamera == null)
        {
            Debug.LogError(
                "BackgroundSpawner: Missing player or camera reference!"
            );

            enabled = false;
            return;
        }

        // Initialise player positions
        prevPosX = player.transform.position.x;
        prevPosY = player.transform.position.y;

        furthestPlayerX = player.transform.position.x;

        // Initialise spawn timer
        lastSpawnTime = Time.time;

        // Initialise anti-lingering
        lingerStartPosition = player.transform.position;
        lingerTimer = 0f;
        playerLingering = false;

        UpdateScreenBounds();

        // Initial spawning
        if (spawnAtStart)
        {
            SpawnInitialObjects();
        }
    }


    void Update()
    {
        UpdateScreenBounds();

        // Check if player is lingering
        CheckPlayerLingering();

        Vector3 currentPosition = player.transform.position;

        // Calculate player movement
        Vector3 movement = new Vector3(
            currentPosition.x - prevPosX,
            currentPosition.y - prevPosY,
            0f
        );

        bool isMoving =
            movement.sqrMagnitude >
            movementThreshold * movementThreshold;

        // Check furthest position
        bool exceededFurthestPosition =
            currentPosition.x > furthestPlayerX;

        // Only spawn when all conditions are met
        if (!playerLingering &&
            isMoving &&
            movement.x > movementThreshold &&
            exceededFurthestPosition &&
            Time.time >= lastSpawnTime + delay)
        {
            if (worldObjectList != null &&
                worldObjectList.Count > 0)
            {
                int index = GetRandomIndex();

                SpawnPowerup(index, movement);
            }

            // Update furthest position
            furthestPlayerX = currentPosition.x;

            // Reset spawn timer
            lastSpawnTime = Time.time;
        }

        // Update previous position
        prevPosX = currentPosition.x;
        prevPosY = currentPosition.y;
    }


    /// <summary>
    /// Detects whether the player is lingering in a small area.
    /// </summary>
    void CheckPlayerLingering()
    {
        Vector3 currentPosition = player.transform.position;

        // Calculate distance from where lingering started
        float distanceMoved = Vector2.Distance(
            new Vector2(
                currentPosition.x,
                currentPosition.y
            ),
            new Vector2(
                lingerStartPosition.x,
                lingerStartPosition.y
            )
        );

        // Player has moved far enough to leave the area
        if (distanceMoved >= lingerMovementDistance)
        {
            lingerStartPosition = currentPosition;

            lingerTimer = 0f;
            playerLingering = false;
        }
        else
        {
            lingerTimer += Time.deltaTime;

            if (lingerTimer >= maxLingerTime)
            {
                playerLingering = true;
            }
        }
    }


    /// <summary>
    /// Calculate camera boundaries in world space.
    /// </summary>
    void UpdateScreenBounds()
    {
        float distance = Mathf.Abs(
            player.transform.position.z -
            mainCamera.transform.position.z
        );

        Vector3 bottomLeft =
            mainCamera.ViewportToWorldPoint(
                new Vector3(0f, 0f, distance)
            );

        Vector3 topRight =
            mainCamera.ViewportToWorldPoint(
                new Vector3(1f, 1f, distance)
            );

        screenLeft = bottomLeft.x;
        screenRight = topRight.x;

        screenBottom = bottomLeft.y;
        screenTop = topRight.y;
    }


    /// <summary>
    /// Spawn objects randomly inside the screen at game start.
    /// </summary>
    void SpawnInitialObjects()
    {
        if (worldObjectList == null ||
            worldObjectList.Count == 0)
        {
            Debug.LogWarning("World Object List is empty!");
            return;
        }

        float minY = Mathf.Max(screenBottom, minimumHeight);
        float maxY = Mathf.Min(screenTop, maximumHeight);

        if (minY > maxY)
            return;

        for (int i = 0; i < initialSpawnCount; i++)
        {
            int index = GetRandomIndex();

            float randomX = Random.Range(
                screenLeft,
                screenRight
            );

            float randomY = Random.Range(minY, maxY);

            Vector3 position = new Vector3(
                randomX,
                randomY,
                offsetZ
            );

            Instantiate(
                worldObjectList[index],
                position,
                worldObjectList[index].transform.rotation
            );
        }
    }


    /// <summary>
    /// Select a random prefab without repeating the previous one.
    /// </summary>
    int GetRandomIndex()
    {
        if (worldObjectList.Count <= 1)
            return 0;

        int index;

        do
        {
            index = Random.Range(0, worldObjectList.Count);
        }
        while (index == lastIndex);

        lastIndex = index;

        return index;
    }


    /// <summary>
    /// Spawn an object based on player movement direction.
    /// </summary>
    public void SpawnPowerup(int index, Vector3 movement)
    {
        if (index < 0 || index >= worldObjectList.Count)
            return;

        GameObject selectedObject = worldObjectList[index];

        List<Vector3> positions =
            CalculateObjectPositions(movement);

        foreach (Vector3 position in positions)
        {
            Instantiate(
                selectedObject,
                position,
                selectedObject.transform.rotation
            );
        }
    }


    /// <summary>
    /// Calculate spawn positions based on movement direction.
    /// </summary>
    List<Vector3> CalculateObjectPositions(Vector3 movement)
    {
        List<Vector3> positions = new List<Vector3>();

        float minY = Mathf.Max(screenBottom, minimumHeight);
        float maxY = Mathf.Min(screenTop, maximumHeight);

        // Detect flying left
        bool bFlyingLeft =
            movement.x < -movementThreshold;

        // Flying left = no spawning
        if (bFlyingLeft)
            return positions;


        // Moving right
        if (movement.x > movementThreshold &&
            minY <= maxY)
        {
            float randomY = Random.Range(minY, maxY);

            positions.Add(new Vector3(
                screenRight + spawnDistanceX,
                randomY,
                offsetZ
            ));
        }


        // Flying upwards
        if (movement.y > movementThreshold &&
            !bFlyingLeft)
        {
            float randomX = Random.Range(
                screenLeft,
                screenRight
            );

            positions.Add(new Vector3(
                randomX,
                screenTop + spawnDistanceY,
                offsetZ
            ));
        }


        // Flying downwards
        if (movement.y < -movementThreshold &&
            !bFlyingLeft)
        {
            float randomX = Random.Range(
                screenLeft,
                screenRight
            );

            positions.Add(new Vector3(
                randomX,
                screenBottom - spawnDistanceY,
                offsetZ
            ));
        }

        return positions;
    }
}
