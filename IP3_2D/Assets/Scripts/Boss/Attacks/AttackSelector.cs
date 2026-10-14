using UnityEngine;
using static UnityEngine.Audio.ProcessorInstance.AvailableData;

public class AttackSelector : MonoBehaviour
{
    [Header("Attack Prefabs")]
    [SerializeField] private IceProjectile icePrefab;
    [SerializeField] private PlasmaProjectile plasmaPrefab;
    [SerializeField] private FireProjectile firePrefab;


    [Header("Attack Fire Settings")]
    [SerializeField] private AttackSettings singleFireSettings;
    [SerializeField] private AttackSettings multipleShotsSettings;
    [SerializeField] private AttackSettings spreadShotSettings;
    [SerializeField] private AttackSettings superAttackSettings;

    [SerializeField] private SingleFireAttack singleFire;
    [SerializeField] private MultipleFireAttack multipleShots;
    [SerializeField] private SpreadShotAttack spreadShot;
    [SerializeField] private SuperAttack superAttack;

    public AttackDecision SelectAttack()
    {
        AttackType attackType = DetermineAttackType();
        AttackElementType element = DetermineAttackElement();
        //AttackSettings settings = GetSettings(attackType);
        Projectile prefab = GetPrefab(element);
        AttackDecision decision = CreateDecision(attackType, element, prefab);
        decision.Settings.isTargeted = ShouldTrackTarget();


        return decision;
    }

    /// <summary>
    /// testing method
    /// </summary>
    /// <param name="forcedType"></param>
    /// <returns></returns>
    public AttackDecision SelectTestAttack(AttackType? forcedType = null, bool isTargeted = false)
    {
        AttackType type = forcedType ?? DetermineAttackType();
        AttackElementType element = AttackElementType.Fire;
        Projectile prefab = GetPrefab(element);

        AttackDecision decision = CreateDecision(type, element, prefab);
        decision.Settings.isTargeted = isTargeted;

        return decision;

    }

    private AttackType DetermineAttackType()
    {
        return (AttackType)Random.Range(1, 4);
    }

    private AttackElementType DetermineAttackElement()
    {
        return (AttackElementType)Random.Range(0, 3);
    }

    public bool ShouldTrackTarget()
    {
        return Random.value < 0.5f;
    }


    private AttackDecision CreateDecision(AttackType type, AttackElementType element, Projectile prefab)
    {
        Attack attack;
        AttackSettings settings;

        switch (type)
        {
            case AttackType.SingleFire:
                attack = singleFire;
                settings = singleFireSettings;

                
                break;

            case AttackType.MultipleShots:
                attack = multipleShots;
                settings = multipleShotsSettings;
                break;

            case AttackType.SpreadShot:
                attack = spreadShot;
                settings = spreadShotSettings;
                break;

            case AttackType.SuperAttack:
                attack = superAttack;
                settings = superAttackSettings;
                break;

            default:
                attack = singleFire;
                settings = singleFireSettings;
                type = AttackType.SingleFire;
                break;
        }

        return new AttackDecision(
            attack,
            type,
            element,
            settings,
            prefab
        );
    }

    /// <summary>
    /// Returns the attack prefab based on the provided attack element type. This method uses a switch expression to select the appropriate prefab for the given element type.
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    private Projectile GetPrefab(AttackElementType element)
    {
        return element switch
        {
            AttackElementType.Fire => firePrefab,
            AttackElementType.Ice => icePrefab,
            AttackElementType.Plasma => plasmaPrefab,
            _ => firePrefab
        };
    }
}
