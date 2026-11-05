using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls spawn intervals for each category.
/// </summary>
public class SpawnScheduler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpawnManager spawnManager;

    [Header("Spawn Rules")]
    [SerializeField] private List<SpawnRule> spawnRules = new();

    private void Update()
    {
        if (!spawnManager.CanSpawn)
            return;

        UpdateSchedules();
    }

    private void UpdateSchedules()
    {
        foreach (SpawnRule rule in spawnRules)
        {
            rule.timer += Time.deltaTime;

            if (rule.timer >= rule.spawnInterval)
            {
                rule.timer = 0f;

                rule.spawner.TrySpawn(spawnManager);
            }
        }
    }
}