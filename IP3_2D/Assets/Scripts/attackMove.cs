using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMove : MonoBehaviour
{
    public float attackSpeed;

    // Start is called before the first frame update
    void Start()
    {
        attackSpeed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += -transform.right * attackSpeed * Time.deltaTime;
    }
}
