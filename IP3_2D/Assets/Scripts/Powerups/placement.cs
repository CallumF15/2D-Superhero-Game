using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class placement : MonoBehaviour
{
	GameObject powerObject, player;
	public List<GameObject> powerArray;
	List<GameObject> spawnedList;
	List<int> aList;

	public float powerupOffsetX = 50.0f;
	public float offsetY = 5.0f;

	float timeElapsed;
	float lastStateCheck = 0f;
	float lastStateCheck2 = 0f;
	public float delay = 5.0f;

	private float currentPosY, prevPosY, currentPosX, prevPosX;
	float lastValue;

	// Use this for initialization
	void Start ()
	{
		aList = new List<int> ();
		spawnedList = new List<GameObject> ();
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

			despawnPowerup();
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
		Vector3 position = calcPowerupPosition ();

		GameObject blah = Instantiate (selectedObject, position, selectedObject.transform.rotation) as GameObject;	
		blah.transform.DetachChildren ();
		spawnedList.Add (blah);

		//despawnPowerup ();
	}

	void despawnPowerup ()
	{
		//Check currentObjects position to player
		//if greater than offset, destroy position

		for (int i = 0; i < spawnedList.Count; i++) {
			float posX = player.transform.position.x;
			float objectPosX = spawnedList [i].transform.position.x;

			if (posX > objectPosX + 30) {
				Destroy (spawnedList [i]);
				spawnedList.RemoveAt(i);
			}
		}
	}

	/// <summary>
	/// Calculates the position for each powerup and coin
	/// </summary>
	Vector3 calcPowerupPosition ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		Vector3 powerupPosition = new Vector3 ();
		playerHeight heightLimit = new playerHeight ();
		float height = heightLimit.playerHeightLimit;
		Debug.Log ("max height limit" + height);
		float groundLevel = 0;


		currentPosY = player.transform.position.y;
		//Debug.Log ("posY " + currentPosY);

		if (currentPosY > prevPosY && currentPosY <= height - 10.0f) { 	//we are going up the way
			if (lastStateCheck + delay < timeElapsed) { 
				Debug.Log ("up way");
				//if going up for 5 seconds, activate power-up spawn
				powerupPosition.y = currentPosY + 5;
				//lastStateCheck = timeElapsed;
				//prevPosY = currentPosY;
			}
		}

		if (currentPosY < prevPosY && currentPosY >= groundLevel + 10.0f) {		//we are going down the way
			if (lastStateCheck + delay < timeElapsed) { 
				Debug.Log ("down way");
				//if going up for 5 seconds, activate power-up spawn
				powerupPosition.y = currentPosY - 5;
				//lastStateCheck = timeElapsed;
				//prevPosY = currentPosY;
			}
		}

		if (currentPosY >= (height - (offsetY * 2)) && currentPosY < height - offsetY) {
			//below max height
			Debug.Log ("max height");
			powerupPosition.y = currentPosY - offsetY;
		}

		if (currentPosY >= groundLevel + offsetY && currentPosY < groundLevel + offsetY * 2) {
			//above min height
			Debug.Log ("min height");
			powerupPosition.y = currentPosY + offsetY;
		}

		if (currentPosY > prevPosY && currentPosY < height - 10.0f) { 	//we are going up the way
			if (lastStateCheck + delay < timeElapsed) { 
				Debug.Log ("up way");
				//if going up for 5 seconds, activate power-up spawn
				powerupPosition.y = currentPosY + 5;
				//lastStateCheck = timeElapsed;
				//prevPosY = currentPosY;
			}
		}

		if (currentPosY < prevPosY && currentPosY > groundLevel + 10.0f) {		//we are going down the way
			if (lastStateCheck + delay < timeElapsed) { 
				Debug.Log ("down way");
				//if going up for 5 seconds, activate power-up spawn
				powerupPosition.y = currentPosY - 5;
				//lastStateCheck = timeElapsed;
				//prevPosY = currentPosY;
			}
		}

		if (currentPosY > height - offsetY * 2 && currentPosY < height - offsetY) {
			//below max height
			Debug.Log ("max height");
			powerupPosition.y = currentPosY - offsetY;
		}

		if (currentPosY > groundLevel + offsetY && currentPosY < groundLevel + offsetY * 2) {
			//above min height
			Debug.Log ("min height");
			powerupPosition.y = currentPosY + offsetY;
		}

		powerupPosition.x = player.transform.position.x + powerupOffsetX;
		powerupPosition.z = 0;
		prevPosY = currentPosY;
		return powerupPosition;
	}
}

