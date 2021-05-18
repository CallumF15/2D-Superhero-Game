using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : MonoBehaviour
{
    private float timeElapsed;
    //[Range(3,8)]
    public float timeDelay = 5;

    private float currentHealth;

    bool hasCollided;

    // Start is called before the first frame update
    void Start()
    {
        hasCollided = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (hasCollided == true)
            ActivateImmunity();
    }

    /// <summary>
    /// Sets the player's health constantly to one
    /// </summary>
    private void ActivateImmunity()
    {
        if (timeElapsed < timeDelay)
            healthbar.SetHealthBarValue(currentHealth);
        else
            Destroy(GetComponent<ShieldPowerup>());
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "shield")
        {
            currentHealth = healthbar.GetHealthBarValue();
            hasCollided = true;
        }
    }
}
