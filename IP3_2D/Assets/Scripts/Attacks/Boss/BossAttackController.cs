using System;
using System.Collections;
using System.Text.Json.Serialization;
using UnityEngine;

public class BossAttackController : MonoBehaviour
{
    [SerializeField] private BossStateManager phaseManager;

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
    [SerializeField] private AttackElementType attackElementType;

    private void Start()
    {
        if (EnableTesting)
            StartCoroutine(TestAttacks());    //Start the attack test loop
        else
            StartCoroutine(BossAttackLoop()); //Start the attack loop
    }

    private IEnumerator TestAttacks()
    {
        while (true)
        {
            if (attackType.Length > 0)
            {
                AttackDecision decision = attackSelector.SelectTestAttack(
                    attackType[0],
                    attackElementType,
                    EnableTargetedAttack
                );

                yield return StartCoroutine(ExecuteAttack(decision));
            }
            else
            {
                yield return null;
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

        // Use the current phase to adjust the before the next attack.
        float phaseMultiplier = 1f;

        if (phaseManager != null && phaseManager.CurrentPhase != null)
        {
            decision.Settings = phaseManager.ApplyPhaseModifiers(decision.Settings);
            phaseMultiplier = phaseManager.CurrentPhase.attackDelayMultplier;
        }

        // Perform the selected attack
        yield return StartCoroutine(
            decision.attack.Execute(
                decision.Settings,
                attackPoint,
                decision.Prefab,
                attackContainer
            )
        );

        float adjustedDelay = delayBetweenAttacks * phaseMultiplier; //can increase or decrease delay between attacks 

        // Wait until the boss can attack again.
        yield return StartCoroutine(AttackTimer.WaitForNextAttack(adjustedDelay));
    } 
}
