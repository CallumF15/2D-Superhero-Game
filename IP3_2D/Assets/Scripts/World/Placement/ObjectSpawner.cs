using UnityEngine;

/// <summary>
/// Handles prefab selection and spawning.
/// </summary>
public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private SpawnPositionProvider positionProvider;
    [SerializeField] private float spawnChance = 1f;
    [SerializeField] private Transform objectContainer;

    public void TrySpawn(SpawnManager spawnManager)
    {
        if (prefabs == null || prefabs.Length == 0)
            return;

        if (Random.value > spawnChance)
            return;

        Spawn(spawnManager);
    }

    private void Spawn(SpawnManager spawnManager)
    {
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

        Vector3 spawnPosition = positionProvider.GetSpawnPosition(spawnManager.HorizontalDirection, spawnManager.VerticalDirection);

        Instantiate(prefab, spawnPosition, Quaternion.identity, objectContainer); //add container here
    }
}
