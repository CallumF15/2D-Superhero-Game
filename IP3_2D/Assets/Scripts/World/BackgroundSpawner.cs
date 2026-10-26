using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

//TODO: Get player's further position travelled and not spawn coins before that point.

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> worldObjectList;

    public float powerupOffsetX = 50.0f;
    public float offsetY = 30.0f;
    public float offsetZ = 0;

    private float timeElapsed;
    private float lastStateCheck = 0f;
    public float delay = 5.0f; //delay between each spawn

    private float currentPosY, prevPosY, currentPosX, prevPosX;
    private float lastValue;

    [SerializeField] private float maximumHeight = 1500f;
    [SerializeField] private float minimumHeight = 0;

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

        if (currentPosX > prevPosX)
        { //make sure player isn't going back the way

            if (lastStateCheck + delay < timeElapsed)
            {
                if (worldObjectList.Count > 1) //Ensure there is at least one value in the list
                {
                    int index = Random.Range(0, worldObjectList.Count);

                    while (lastValue == index)
                    {
                        index = Random.Range(0, worldObjectList.Count);
                    }

                    lastValue = index;
                    SpawnPowerup(index);
                }

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
        GameObject selectedObject = worldObjectList[index];
        currentPosX = player.transform.position.x;
        Vector3 position = CalculateObjectPosition();
        GameObject worldObject = Instantiate(selectedObject, position, selectedObject.transform.rotation) as GameObject;    //spawn object
    }

    /// <summary>
    /// Calculates the position for specified background object in the world
    /// </summary>
    Vector3 CalculateObjectPosition()
    {
        Vector3 powerupPosition = new Vector3();
        powerupPosition.x = player.transform.position.x + powerupOffsetX; //so coins/powerups spawn off screen
        powerupPosition.z = 0 + offsetZ;

        //player is flying up and is below the maximum height
        if (currentPosY >= prevPosY && currentPosY <= maximumHeight + 2) 
        {
            prevPosY = currentPosY;

            if (currentPosY - 10 >= minimumHeight)
            {
                powerupPosition.y = currentPosY - 10;
            }
            else
            {
                float difference = minimumHeight + currentPosY;
                powerupPosition.y = currentPosY - difference;
            }

            return powerupPosition;
        }

        //Player is flying down and is above the minimum height
        if (currentPosY < prevPosY && currentPosY >= minimumHeight - 2)
        {   
            prevPosY = currentPosY;

            if(currentPosY + 10 <= maximumHeight)
            {
                powerupPosition.y = currentPosY + 10;
            }
            else
            {
                float difference = maximumHeight - currentPosY;
                powerupPosition.y = currentPosY + difference;
            }

            return powerupPosition;
        }

        return powerupPosition;
    }
}

