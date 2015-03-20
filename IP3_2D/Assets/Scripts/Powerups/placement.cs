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
	public float offsetY = 30.0f;

	float timeElapsed;
	float lastStateCheck = 0f;
	public float delay = 5.0f;

	private float currentPosY, prevPosY, currentPosX, prevPosX;
	float lastValue;

	private float groundLevel = 0;

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
	}

	void despawnPowerup ()
	{
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

		currentPosY = player.transform.position.y;

		if (currentPosY > prevPosY && currentPosY <= height - offsetY) { 	//we are going up the way
				powerupPosition.y = currentPosY + 5;
		}

		if (currentPosY < prevPosY && currentPosY >= groundLevel + offsetY) {		//we are going down the way
				powerupPosition.y = currentPosY - 5;
		}

		if (powerupPosition.y >= groundLevel && powerupPosition.y <= groundLevel + offsetY) {
			powerupPosition.y = powerupPosition.y + 10.0f;
		}

		if (powerupPosition.y <= height && powerupPosition.y >= height - offsetY) {
			powerupPosition.y = height - 10.0f;
		}

		Debug.Log ("POWERUP Y POS " + powerupPosition.y);
	
		powerupPosition.x = player.transform.position.x + powerupOffsetX;
		powerupPosition.z = 0;
		prevPosY = currentPosY;
		return powerupPosition;
	}
}

