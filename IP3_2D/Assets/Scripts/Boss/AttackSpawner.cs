using UnityEngine;

public class AttackSpawner : MonoBehaviour
{

    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private Transform spawnPoint;

    public void SpawnAttack()
    {
        Instantiate(
            attackPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );
    }
}
