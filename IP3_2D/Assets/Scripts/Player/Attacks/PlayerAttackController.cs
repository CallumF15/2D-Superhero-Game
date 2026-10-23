
using UnityEngine;

using UnityEngine.InputSystem;


public class PlayerAttackController : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float distanceToSpawn = 3f;
    [SerializeField] private float attackCooldown = 0.5f;


    [Header("Projectiles")]
    [SerializeField] private Projectile projectile;
    [SerializeField] private IceProjectile iceProjectile;
    [SerializeField] private Projectile currentProjectile;

    [SerializeField] private Transform attackContainer;
    [SerializeField] private AttackSettings attackSettings;


    private float nextAttackTime;

    void Start() { 
   
    }


    /// <summary>
    /// allows player to fire via clicking, or holding down but restricted by the attack cooldown delay
    /// </summary>
    void Update()
    {
        bool attackInput = (Mouse.current != null && Mouse.current.leftButton.isPressed) || (Keyboard.current != null && Keyboard.current.spaceKey.isPressed);

        if (!attackInput)
            return;

        if (Time.time < nextAttackTime)
            return;

        Attack();
        nextAttackTime = Time.time + attackCooldown;
    }


    private void Attack()
    {
        if (projectile == null)
        {
            Debug.LogWarning("Projectile prefab or fire point is missing.", this);
            return;
        }

        SpawnProjectile();
    }

    private void SpawnProjectile()
    {
        // Get the direction the player is currently facing.
        Vector3 playerDirection = transform.right;

        // Calculate where the attack should  Start at the player and move forward  distanceToSpawn units.
        Vector3 spawnPosition = transform.position + playerDirection * distanceToSpawn;

        // Create a new copy of the attack prefab at the calculated position and rotation.
        Projectile spawnedProjectile = Instantiate(projectile, spawnPosition, transform.rotation, attackContainer);

        spawnedProjectile.Initialize(attackSettings.damage, attackSettings.shotSpeed, playerDirection, gameObject);
    }
}
