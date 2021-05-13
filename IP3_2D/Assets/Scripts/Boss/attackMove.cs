using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += -transform.right * 3 * Time.deltaTime;
    }
}
