using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;

 

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


    public virtual void Initialize(
        float damage,
        float speed)
    {
        this.damage = damage;
        this.speed = speed;
    }

    protected abstract void TakeDamage(GameObject target);


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Object collided with: " + collision.gameObject.name);

        if (!collision.gameObject.CompareTag(targetName))
            return;


        Debug.Log("Player attack hit boss!");
        TakeDamage(collision.gameObject);
        Destroy(gameObject);
    }
}