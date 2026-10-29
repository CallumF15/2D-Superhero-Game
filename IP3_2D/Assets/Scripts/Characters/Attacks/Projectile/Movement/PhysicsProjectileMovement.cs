using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsProjectileMovement : MonoBehaviour, IProjectileMovement
{
    private float projectileSpeed; //how fast attack moves

    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
        set { projectileSpeed = value; }
    }
    private IMoveable movement;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Projectile is missing Rigidbody2D!", this);
        }
    }

    public void Initialize(float speed, Vector2 direction, GameObject character)
    {
        if (rb == null) return;

        this.projectileSpeed = speed;

        movement = character.GetComponent<IMoveable>();
        rb.gravityScale = 0f;
        rb.linearDamping = 0f;
        rb.linearVelocity = direction * projectileSpeed + movement.CurrentVelocity;
    }
}
