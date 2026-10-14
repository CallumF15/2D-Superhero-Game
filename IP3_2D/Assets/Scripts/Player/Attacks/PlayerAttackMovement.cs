using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackMovement : MonoBehaviour
{
    public float speed = 5; //how fast attack moves

    // Start is called before the first frame update
    private void Start()
    {
        Collider2D myCollider = GetComponent<Collider2D>();

        foreach (GameObject attack in GameObject.FindGameObjectsWithTag("playerAttack")) //ignore the other player attacks to prevent collision
        {
            if (attack == gameObject)
                continue;

            Collider2D otherCollider = attack.GetComponent<Collider2D>();

            if (otherCollider != null)
            {
                Physics2D.IgnoreCollision(myCollider, otherCollider);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed; //move in direction of angle
    }
}
