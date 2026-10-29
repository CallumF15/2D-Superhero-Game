using UnityEngine;

public class BossStateManager : MonoBehaviour
{
    [SerializeField] private BossPhase[] phases;

    public BossPhase CurrentPhase { get; private set; }

    public void UpdatePhase(float currentHealth, float maxHealth)
    {
        if (maxHealth <= 0f || phases == null || phases.Length == 0)
            return;

        float healthPercent = currentHealth / maxHealth * 100f;

        BossPhase selectedPhase = phases[0];

        // Assumes phases are ordered from health threshold to lowest.
        foreach (BossPhase phase in phases)
        {
            if (healthPercent <= phase.healthThreshold)
                selectedPhase = phase;
        }

        if (CurrentPhase != selectedPhase)
        {
            CurrentPhase = selectedPhase;
            Debug.Log("Boss entered: " + CurrentPhase.phaseName);
        }
    }

    public AttackSettings ApplyPhaseModifiers(AttackSettings original)
    {
        AttackSettings modified = original;

        modified.damage *= CurrentPhase.damageMultiplier;
        modified.shotSpeed *= CurrentPhase.projectileSpeedMultiplier;
        modified.shotCount += CurrentPhase.extraShots;
        modified.shotDelay += CurrentPhase.attackDelayMultplier;

        return modified;
    }
}