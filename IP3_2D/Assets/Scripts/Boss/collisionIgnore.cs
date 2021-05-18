using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionIgnore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] attackObject = GameObject.FindGameObjectsWithTag("bossAttack");

        foreach(GameObject obj in attackObject)
        Physics.IgnoreCollision(obj.GetComponent<Collider>(), GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
