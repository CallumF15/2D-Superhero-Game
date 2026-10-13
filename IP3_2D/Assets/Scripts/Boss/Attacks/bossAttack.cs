using System;
using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("Timing")]
    [SerializeField] private float startGameDelay = 10f;
    [SerializeField] private float delayBetweenAttacks = 5f;

    [Header("Projectile Prefabs")]
    [SerializeField] private GameObject[] attackPrefabs;

    [Header("Attack Settings")]
    [SerializeField] private AttackSettings[] attacks;

    [Header("Spawn")]
    [SerializeField] private Vector3 spawnOffset = new Vector3(-1f, 0f, 0f);

    private AttackSettings currentAttack;

    private void Start()
    {
        StartCoroutine(BossAttackLoop());
    }

    private IEnumerator BossAttackLoop()
    {
        yield return new WaitForSeconds(startGameDelay);

        while (true)
        {
            ChooseAttack();

            if (currentAttack != null)
            {
                yield return StartCoroutine(PerformAttack());
            }

            yield return new WaitForSeconds(delayBetweenAttacks);
        }
    }

    private int GetBossPhase(float healthPercent)
    {
        if (healthPercent <= 0.25f)
            return 3;

        if (healthPercent <= 0.50f)
            return 2;

        if (healthPercent <= 0.75f)
            return 1;

        return 0;
    }

    private void ChooseAttack()
    {
        if (attacks == null || attacks.Length == 0)
        {
            Debug.LogWarning("No boss attacks have been configured.", this);
            currentAttack = null;
            return;
        }

        //currentAttack = attacks[Random.Range(0, attacks.Length)];

        
    }

    private IEnumerator PerformAttack()
    {
        for (int i = 0; i < currentAttack.shotCount; i++)
        {
            FireAttack();

            yield return new WaitForSeconds(currentAttack.shotDelay);
        }
    }

    private void FireAttack()
    {
        //GameObject projectilePrefab

        //if (projectilePrefab == null)
        //{
        //    Debug.LogWarning("No boss projectile prefab is configured.", this);
        //    return;
        //}

        //Vector3 spawnPosition = transform.position + spawnOffset;

        //float[] angles = currentAttack.angles;

        //if (angles == null || angles.Length == 0)
        //{
        //    angles = new float[] { 0f };
        //}

        //foreach (float angle in angles)
        //{
        //    SpawnProjectile(projectilePrefab, spawnPosition, angle);
        //}
    }

    private void SpawnProjectile(GameObject projectilePrefab, Vector3 position, float angle)
    {
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        GameObject projectile = Instantiate(projectilePrefab, position, rotation);

        AttackMove attackMove = projectile.GetComponent<AttackMove>();

        if (attackMove == null)
        {
            attackMove = projectile.AddComponent<AttackMove>();
        }

        // These lines depend on your AttackMove script.
        //
        // attackMove.SetSpeed(currentAttack.shotSpeed);
        // attackMove.SetTargeted(currentAttack.isTargeted);
    }

    //private GameObject GetRandomProjectile()
    //{
    //    if (attackPrefabs == null || attackPrefabs.Length == 0)
    //        return null;

    //    return attackPrefabs[Random.Range(0, attackPrefabs.Length)];
    //}
}