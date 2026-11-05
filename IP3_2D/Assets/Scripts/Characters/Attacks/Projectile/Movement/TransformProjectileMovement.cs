using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UIElements;

public class TransformProjectileMovement : MonoBehaviour, IProjectileMovement
{
    private float projectileSpeed; //how fast attack moves
    private Vector2 direction;

    private Vector2 inherited;


    private GameObject owner;

    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
        set { projectileSpeed = value; }
    }

    private void Start()
    {
        //inheritedVerticalSpeed = GameObject.FindGameObjectWithTag("boss").GetComponent<BossMovement>().VerticalSpeed;
    }



    public void Initialize(float speed, Vector2 direction, GameObject owner)
    {
        projectileSpeed = speed;
        this.direction = direction.normalized;
        this.owner = owner;

        var mover = owner.GetComponent<IMoveable>();
        if (mover != null)
            inherited = mover.CurrentVelocity;
    }

    private void Update()
    {
        //FireAtPlayerWithVerticalSpeed();
        //transform.position += (Vector3)(-direction * projectileSpeed * Time.deltaTime);

        //Vector2 move = -direction * projectileSpeed;
        //Debug.DrawRay(transform.position, move.normalized * 5f, Color.red);
        //Debug.Log($"proj pos {transform.position}  frame {Time.frameCount}");


        Vector2 move = -direction * projectileSpeed + inherited;
        transform.position += (Vector3)(move * Time.deltaTime);
    }
}
