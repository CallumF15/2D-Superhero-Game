using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Projectile : MonoBehaviour
{
    protected float damage;
    protected float speed;

    [SerializeField] private LayerMask destroyedBy;

    private ProjectileMovement movement;

    private void Awake()
    {
        movement = GetComponent<ProjectileMovement>();
    }

    public enum TargetTag
    {
        Player,
        Boss
    }

    [SerializeField] private TargetTag targetTag;
    private string targetName;

    private void Start()
    {
        if (targetTag == TargetTag.Player)
        {
            Debug.Log("Target is Player");
            targetName = "Player";
        }
        else if (targetTag == TargetTag.Boss)
        {
            Debug.Log("Target is Boss");
            targetName = "boss";
        } 
    }

    /// <summary>
    /// Sets the speed in the projectile movement class
    /// Sets the damage to be used in the collision
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="speed"></param>
    /// <param name="direction"></param>
    /// <param name="character"></param>
    public virtual void Initialize(float damage, float speed, Vector2 direction, GameObject character)
    {
        this.damage = damage;
        this.speed = speed;

        if (movement != null)
        {
            movement.Initialize(direction, speed, character);
        }
    }

    public void InitializeBoss(float damage, float speed)
    {
        this.damage = damage;
        this.speed = speed;

        GetComponent<BossAttackMovement>().ProjectileSpeed = speed;
    }

    protected abstract void TakeDamage(GameObject target, float damage, float speed);



    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object collided with: " + other.gameObject.name);

        if ((destroyedBy.value & (1 << other.gameObject.layer)) != 0)
        {
            Destroy(gameObject);
        }

        if (!other.gameObject.CompareTag(targetName))
            return;

        TakeDamage(other.gameObject, this.damage, this.speed);
        Destroy(gameObject);
    }
}