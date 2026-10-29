using UnityEngine;


using UnityEngine;

public class BossGunTargeting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;

    [SerializeField] private TransformProjectileMovement bossAttackMovement;

    [Header("Aim Settings")]
    [SerializeField] private float maxRotation = 45f;
    [SerializeField] private float rotationSpeed = 5f;

    [Header("Accuracy Settings")]
    [SerializeField, Range(0f, 1f)]
    private float aimAccuracy = 0.8f;

    [SerializeField] private float aimOffset = 180f;
    private Vector2 playerLastPosition;
    private Vector2 playerVelocity;

    private float initialRotation;

    public float projectileSpeed;

    public void SetProjectileSpeed(float speed)
    {
        projectileSpeed = speed;
    }

    private void Awake()
    {
        initialRotation = transform.localEulerAngles.z;
    }

    private void Update()
    {
        if (player == null)
            return;

        AimAtPlayer();
    }

    private void AimAtPlayer()
    {
        Vector2 direction = player.position - transform.position;

        float targetAngle = Mathf.Atan2(
            direction.y,
            direction.x
        ) * Mathf.Rad2Deg;

        targetAngle += aimOffset;

        // Calculate angle relative to starting rotation
        float relativeAngle = Mathf.DeltaAngle(
            initialRotation,
            targetAngle
        );

        // Restrict rotation
        relativeAngle = Mathf.Clamp(
            relativeAngle,
            -maxRotation,
            maxRotation
        );

        float finalAngle = initialRotation + relativeAngle;

        Quaternion targetRotation = Quaternion.Euler(
            0f,
            0f,
            finalAngle
        );

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * 100f * Time.deltaTime
        );
    }

    private void AimAtPlayer2()
    {
        if (player == null) return;

        // Estimate the player's movement speed
        Vector2 currentPosition = player.position;

        playerVelocity = (currentPosition - playerLastPosition) / Time.deltaTime;

        playerLastPosition = currentPosition;

        // Direction and distance to the player
        Vector2 direction = (Vector2)player.position - (Vector2)transform.position;

        float distance = direction.magnitude;

        // Estimate how long the projectile will take to reach the player
        float travelTime = distance / bossAttackMovement.ProjectileSpeed;

        // Predict where the player will be
        Vector2 predictedPosition = (Vector2)player.position + playerVelocity * travelTime;

        // Aim at the predicted position
        Vector2 aimDirection = predictedPosition - (Vector2)transform.position;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // Adjust for your gun's artwork orientation
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 180f);
    }
}
