using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TargetedAttack : MonoBehaviour
{
    private float attackSpeed;
    private Transform player;

    [SerializeField] private float rotationSpeed = 180f;

    // Start is called before the first frame update
    void Start()
    {
        attackSpeed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float step = attackSpeed * Time.deltaTime;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        Vector2 playerTarget = player.transform.position;

        // Gradually turn towards the player
        RotateAttackTowardsTarget(playerTarget);

        // Keep moving forwards in the direction the projectile faces
        transform.position += -transform.right * step;
    }



    private void RotateAttackTowardsTarget(Vector2 target)
    {
        Vector2 direction = target - (Vector2)transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Keep the 180-degree offset for your sprite's orientation
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle + 180f);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}
