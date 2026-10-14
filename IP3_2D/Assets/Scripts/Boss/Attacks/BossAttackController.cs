using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using NUnit.Framework;
using UnityEngine;

public class BossAttackController : MonoBehaviour
{
    private readonly int currentPhase; // Current phase of the boss based on health percentage

    [SerializeField] private Vector3 attackSpawnOffset = new Vector3(-1f, 0f, 0f); // Offset for spawning the attack object relative to the boss's position

    [SerializeField] private Transform attackPoint;
    [SerializeField] private AttackSelector attackSelector;
    [SerializeField] private Transform attackContainer;

    [Header("Timing")]
    [SerializeField] private float startGameDelay = 5f; // Delay before the boss starts attacking after the game begins
    [SerializeField] private float delayBetweenAttacks = 5f; // Delay between each attack

    [Header("Testing")]
    [Tooltip("Enable Testing for specific Attacks")]
    [SerializeField] private bool EnableTesting;
    [SerializeField] private bool EnableTargetedAttack;
    [SerializeField] private AttackType[] attackType;


    private void Start()
    {
        if (EnableTesting)
            StartCoroutine(TestAttacks());    //Start the attack test loop
        else
            StartCoroutine(BossAttackLoop()); //Start the attack loop
    }

    private IEnumerator TestAttacks()
    {
        foreach (AttackType attack in attackType)
        {
            while (true)
            {
                AttackDecision decision = attackSelector.SelectTestAttack(attack, EnableTargetedAttack);

                yield return StartCoroutine(ExecuteAttack(decision));
            }
        }
    }


    private IEnumerator BossAttackLoop()
    {
        yield return StartCoroutine(AttackTimer.WaitForStart());

        while (true)
        {
            // Ask the selector what attack to perform.
            AttackDecision decision = attackSelector.SelectAttack();

            if (decision != null)
            {
                Debug.Log(
                    $"Boss selected {decision.Type} " +
                    $"using {decision.Element}" +
                    $" targeting: {decision.Settings.isTargeted}"
                   );
            }


            yield return StartCoroutine(ExecuteAttack(decision));
        }
    }

    private IEnumerator ExecuteAttack(AttackDecision decision)
    {
        if (decision == null)
            yield break;

        //attackPoint.position = transform.position + attackSpawnOffset;

        // Perform the selected attack
        yield return StartCoroutine(
            decision.attack.Execute(
                decision.Settings,
                attackPoint,
                decision.Prefab,
                attackContainer
            )
        );


        // Wait until the boss can attack again.
        yield return StartCoroutine(AttackTimer.WaitForNextAttack(delayBetweenAttacks));
    }

}
