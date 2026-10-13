using UnityEngine;
using System.Collections;

public static class AttackTimer
{

    [Header("Timing")]
    [SerializeField] private static float startGameDelay = 10f;

    public static IEnumerator WaitForNextAttack(float delayBetweenAttacks)
    {
        yield return new WaitForSeconds(delayBetweenAttacks);
    }

    public static IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(startGameDelay);
    }
}