using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class placement : MonoBehaviour
{
	GameObject powerObject, player;
	public List<GameObject> powerArray;
	List<GameObject> spawnedList = new List<GameObject> ();

	public float powerupOffsetX = 50.0f;
	public float offsetY = 30.0f;

	private float timeElapsed;
	private float lastStateCheck = 0f;
	public float delay = 5.0f;

	private float currentPosY, prevPosY, currentPosX, prevPosX;
	private float lastValue;
	private bool isCoin;

	private float groundLevel = 0;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		timeElapsed = Time.time;
		currentPosX = player.transform.position.x;

		if (currentPosX > prevPosX) {
			if (lastStateCheck + delay < timeElapsed) {
				//Debug.Log("COUNT " + powerArray.Count);
				int index = Random.Range (0, powerArray.Count);
			
				while (lastValue == index) {
					index = Random.Range (0, powerArray.Count);
				}

				lastValue = index;

				Debug.Log ("INDEX " + index);

				spawnPowerup (index);
				//powerupSelector ();
				lastStateCheck = timeElapsed;
			}

			prevPosX = currentPosX;
		}
	}
	
	/// <summary>
	/// Spawns the powerup.
	/// </summary>
	public void spawnPowerup (int index)
	{
		GameObject selectedObject = powerArray [index];
		currentPosX = player.transform.position.x;
		Vector3 position = CalculatePowerupPosition();

		GameObject worldObject = Instantiate (selectedObject, position, selectedObject.transform.rotation) as GameObject;	

		if (worldObject.tag == "coin") {
			isCoin = true;
			worldObject.transform.DetachChildren ();
			Destroy (worldObject);
		} else
			isCoin = false;

		spawnedList.Add (worldObject);
	}

	/// <summary>
	/// Calculates the position for each powerup and coin
	/// </summary>
	Vector3 CalculatePowerupPosition ()
	{
		player = GameObject.FindGameObjectWithTag ("Player"); //get player object
		Vector3 powerupPosition = new Vector3 (); 
		playerHeight heightLimit = new playerHeight ();
		float height = heightLimit.playerHeightLimit; //get game level map boundary height

		currentPosY = player.transform.position.y; //get player Y pos

		if (currentPosY > prevPosY && currentPosY <= height - offsetY) { 	//Player is going up
				powerupPosition.y = currentPosY + 5;
		}

		if (currentPosY < prevPosY && currentPosY >= groundLevel + offsetY) {		//Player is going down
				powerupPosition.y = currentPosY - 5;
		}

		if (powerupPosition.y >= groundLevel && powerupPosition.y <= groundLevel + offsetY) {
			powerupPosition.y = powerupPosition.y + 10.0f;
		}

		if (powerupPosition.y <= height && powerupPosition.y >= height - offsetY) {
			powerupPosition.y = height - 10.0f;
		}

		powerupPosition.x = player.transform.position.x + powerupOffsetX;
		powerupPosition.z = 0;
		prevPosY = currentPosY;
		return powerupPosition;
	}
}

