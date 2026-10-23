using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileMovement : MonoBehaviour
{
    private float projectileSpeed; //how fast attack moves

    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
        set { projectileSpeed = value; }
    }
    private PlayerMovement movement;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Projectile is missing Rigidbody2D!", this);
        }
    }

    public void Initialize(Vector2 direction, float speed, GameObject character)
    {
        if (rb == null) return;

        movement = character.GetComponent<PlayerMovement>();
        rb.gravityScale = 0f;
        rb.linearDamping = 0f;
        rb.linearVelocity = direction * speed + movement.CurrentVelocity;
    }
}
