using UnityEngine;
using UnityEngine.UIElements;

public class BossAttackMovement : MonoBehaviour
{
    private float projectileSpeed; //how fast attack moves

    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
        set { projectileSpeed = value; }
    }

    private void Update()
    {
        transform.position += -transform.right * projectileSpeed * Time.deltaTime;
    }
}
