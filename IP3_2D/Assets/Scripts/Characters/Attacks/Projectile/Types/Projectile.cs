using UnityEngine;
public abstract class Projectile : MonoBehaviour
{
    protected float damage;
    protected float speed;

    [SerializeField] private LayerMask destroyedBy;

    IProjectileMovement movement;

    //private ProjectileMovement movement;

    private void Awake()
    {
        movement = GetComponent<IProjectileMovement>();
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
    public virtual void Initialize(float damage, float speed, Vector2 direction, GameObject character)
    {
        this.damage = damage;
        this.speed = speed;

        if (movement != null)
        {
            movement.Initialize(speed, direction, character);
        }
    }

    protected abstract void DealDamage(IDamageable target, Collider2D other);


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object collided with: " + other.gameObject.name);
        IDamageable target = other.GetComponent<IDamageable>(); 

        if ((destroyedBy.value & (1 << other.gameObject.layer)) != 0)
        {
            Destroy(gameObject);
        }

        if (!other.gameObject.CompareTag(targetName))
            return;

        DealDamage(target, other);

        Destroy(gameObject);
    }
}