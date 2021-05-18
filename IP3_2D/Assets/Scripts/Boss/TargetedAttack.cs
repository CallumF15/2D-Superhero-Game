using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedAttack : MonoBehaviour
{
    private float attackSpeed;

    // Start is called before the first frame update
    void Start()
    {
        GameObject bossObject = GameObject.FindGameObjectWithTag("boss");
        attackSpeed = bossObject.GetComponent<bossAttack>().shotSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float step = attackSpeed * Time.deltaTime;
        Vector2 playerTarget = GameObject.FindGameObjectWithTag("Player").transform.position;

        // move object towards the target location
        transform.position = Vector2.MoveTowards(transform.position, playerTarget, step);
        RotateTowards(playerTarget);
    }

    /// <summary>
    /// Rotates the attack to match the player's rotation 
    /// </summary>
    /// <param name="target"></param>
    private void RotateTowards(Vector2 target)
    {
        var offset = 180f;
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}
