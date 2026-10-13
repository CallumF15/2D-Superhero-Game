using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

//TODO: Get player's further position travelled and not spawn coins before that point.

public class Placement : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> powerArray;

    public float powerupOffsetX = 50.0f;
    public float offsetY = 30.0f;

    private float timeElapsed;
    private float lastStateCheck = 0f;
    public float delay = 5.0f; //delay between each spawn

    private float currentPosY, prevPosY, currentPosX, prevPosX;
    private float lastValue;

    private float groundLevel = 0;

    // Use this for initialization
    void Start()
    {
        prevPosY = player.transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeElapsed = Time.time;
        currentPosX = player.transform.position.x;
        currentPosY = player.transform.position.y; //get player Y pos

        if (currentPosY > 100)
            player.transform.position = new Vector3(currentPosX, 100.0f, 0);

        if (currentPosX > prevPosX)
        { //make sure player isn't going back the way

            if (lastStateCheck + delay < timeElapsed)
            {
                //Debug.Log("COUNT: " + powerArray.Count);
                int index = Random.Range(0, powerArray.Count);

                while (lastValue == index)
                {
                    index = Random.Range(0, powerArray.Count);
                }

                lastValue = index;

                //Debug.Log ("INDEX " + index);

                SpawnPowerup(index);


                lastStateCheck = timeElapsed;
            }

            prevPosX = currentPosX;
        }
        else
        {
            //player is going back the way
            lastStateCheck = timeElapsed; //reset time elapsed 
        }
    }

    /// <summary>
    /// Spawns the powerup.
    /// </summary>
    public void SpawnPowerup(int index)
    {
        GameObject selectedObject = powerArray[index];
        currentPosX = player.transform.position.x;
        Vector3 position = CalculatePowerupPosition();
        GameObject worldObject = Instantiate(selectedObject, position, selectedObject.transform.rotation) as GameObject;    //spawn object
    }

    /// <summary>
    /// Calculates the position for each powerup and coin  
    /// </summary>
    Vector3 CalculatePowerupPosition()
    {
        Vector3 powerupPosition = new Vector3();
        powerupPosition.x = player.transform.position.x + powerupOffsetX; //so coins/powerups spawn off screen
        powerupPosition.z = 0;

        float height = HeightRestrictor.playerHeightLimit;
        //Debug.Log("player Y: " + player.transform.position.y);

        if (currentPosY >= prevPosY && currentPosY <= height + 2)
        {
            prevPosY = currentPosY;

            if (currentPosY - 10 >= groundLevel)
            {
                powerupPosition.y = currentPosY - 10;
            }
            else
            {
                float difference = groundLevel + currentPosY;
                powerupPosition.y = currentPosY - difference;
            }

            return powerupPosition;
        }

        if (currentPosY < prevPosY && currentPosY >= groundLevel - 2)
        {   //Player is going down and is above ground level
            prevPosY = currentPosY;

            if(currentPosY + 10 <= height)
            {
                powerupPosition.y = currentPosY + 10;
            }
            else
            {
                float difference = height - currentPosY;
                powerupPosition.y = currentPosY + difference;
            }

            return powerupPosition;
        }

        return powerupPosition;
    }

    Vector3 CalculateCloudPosition()
    {


        return new Vector3(1, 1, 1);
    }
}

