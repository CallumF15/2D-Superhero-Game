using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : MonoBehaviour
{
    [Range(1, 10)]
    public int healthToReplenish;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "powerups")
            if (other.gameObject.name == "Health")
            {
                Healthbar.SetHealthBarValue(Healthbar.GetHealthBarValue() + healthToReplenish);
            }
    }
}
