using UnityEngine;
using UnityEngine.UIElements;

public class TransformProjectileMovement : MonoBehaviour, IProjectileMovement
{
    private float projectileSpeed; //how fast attack moves
    private Vector2 direction;
    private float inheritedVerticalSpeed;

    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
        set { projectileSpeed = value; }
    }

    private void Start()
    {
        inheritedVerticalSpeed = GameObject.FindGameObjectWithTag("boss").GetComponent<BossMovement>().VerticalSpeed;
    }


    private void FireAtPlayerWithVerticalSpeed()
    {
        Vector2 movement = -transform.right * projectileSpeed;

        movement.y += inheritedVerticalSpeed;

        transform.position += (Vector3)(movement * Time.deltaTime);
    }

    public void Initialize(float speed, Vector2 direction, GameObject owner)
    {
        projectileSpeed = speed;
        this.direction = direction.normalized;
    }

    private void Update()
    {
        //transform.position = -transform.right * projectileSpeed * Time.deltaTime;
        transform.position += (Vector3)(-direction * projectileSpeed * Time.deltaTime);
    }
}
