using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class placement : MonoBehaviour
{
	GameObject powerObject, player;
	List<GameObject> powerArray;

	public float distanceBetweenPowerup = 30.0f;
	public float powerupOffset = 50.0f;
	public float offsetY = 5.0f;
	private int numberOfPowerups = 5;

	List<int> aList;
	List<Rigidbody2D> powerupList;

	float timeElapsed;
	float lastStateCheck = 0f;
	public float delay = 5.0f;

	private float currentPosY, prevPosY, currentPosX, prevPosX;

	// Use this for initialization
	void Start ()
	{
		aList = new List<int> ();
		powerupList = new List<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		//getPrefabs ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		timeElapsed = Time.time;
		if (lastStateCheck + delay < timeElapsed) {
			powerupSelector ();
			lastStateCheck = timeElapsed;
		}
	}

	void getPrefabs ()
	{

		//Rigidbody2D healthPickup = Resources.Load ("Powerups/Health") as Rigidbody2D;
		GameObject healthPickup = Resources.Load ("Health") as GameObject;
	
		if (healthPickup == null)
			Debug.Log ("NULL");
		else
			Debug.Log ("NOT NULL");

		//Rigidbody2D shieldPickup = Resources.Load ("IP3_2D_CF/IP3_2D/Assets/Models/Powerups/Shield") as Rigidbody2D;
		//Rigidbody2D healthPickup = Resources.Load ("healthpickup");

		GameObject blah = Instantiate (healthPickup, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		//Rigidbody2D blah = Instantiate(healthPickup, new Vector3(0,0,0), Quaternion.identity) as Rigidbody2D;

	}
	
	/// <summary>
	/// Selects the least use power-up and spawns it
	/// </summary>
	void powerupSelector ()
	{
		int index = Random.Range (0, numberOfPowerups - 1);
		aList.Add (index);

		var q = from x in aList
			group x by x into g
				let count = g.Count ()
				orderby count ascending
		select new {Value = g.Key, Count = count};

		int lastValue = 0;
		int heightestValue = 0;
		int lowestValue = 0;
		int select = 0;

		foreach (var x in q) {
			//Debug.Log ("Value: " + x.Value + " Count: " + x.Count);

			if(x.Count > lastValue)
			{
				if(x.Count > heightestValue){
					heightestValue = x.Count;
				}

				if(x.Count < lowestValue){
					lowestValue = x.Count;
					select = x.Value;
				}

				lastValue = x.Count;
			}
		}
		spawnPowerup (select);
	}

	/// <summary>
	/// Spawns the powerup.
	/// </summary>
	public void spawnPowerup(int index)
	{
		//Rigidbody2D selectedObject = powerupList [index];
		currentPosX = player.transform.position.x;
		Vector3 position = calcPowerupPosition ();

		if (currentPosX > prevPosX) {
			GameObject blah =  Instantiate (GameObject.CreatePrimitive (PrimitiveType.Cube), position, Quaternion.identity) as GameObject;
			blah.AddComponent<Rigidbody>();
			blah.rigidbody.useGravity = false;
			blah.tag = "cubes";
		}
		prevPosX = currentPosX;
	}

	void despawnPowerup()
	{
		for (int i = 0; i < powerArray.Count; i++) {
			float posX = player.transform.position.x;
			float objectPosX = powerArray[i].transform.position.x;

			if(posX > objectPosX){
				Destroy(powerArray[i]);
			}
		}
		Vector3 position = calcPowerupPosition ();
		Instantiate (GameObject.CreatePrimitive(PrimitiveType.Cube), position, Quaternion.identity);
	}

	/// <summary>
	/// Calculates the powerup position.
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
		Debug.Log ("posY " + currentPosY);

			if (currentPosY > prevPosY && currentPosY <= height - 10.0f) { 	//we are going up the way
				if (lastStateCheck + delay < timeElapsed) { 
					Debug.Log ("up way");
					//if going up for 5 seconds, activate power-up spawn
					powerupPosition.y = currentPosY + 5;
					lastStateCheck = timeElapsed;
					//prevPosY = currentPosY;
				}
			}

			if (currentPosY < prevPosY && currentPosY >= groundLevel + 10.0f) {		//we are going down the way
				if (lastStateCheck + delay < timeElapsed) { 
					Debug.Log ("down way");
					//if going up for 5 seconds, activate power-up spawn
					powerupPosition.y = currentPosY - 5;
					lastStateCheck = timeElapsed;
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
				Debug.Log("up way");
				//if going up for 5 seconds, activate power-up spawn
				powerupPosition.y = currentPosY + 5;
				lastStateCheck = timeElapsed;
				prevPosY = currentPosY;
			}
		}

		if (currentPosY < prevPosY && currentPosY > groundLevel + 10.0f) {		//we are going down the way
			if (lastStateCheck + delay < timeElapsed) { 
				Debug.Log("down way");
				//if going up for 5 seconds, activate power-up spawn
				powerupPosition.y = currentPosY - 5;
				lastStateCheck = timeElapsed;
				prevPosY = currentPosY;
			}
		}

		if (currentPosY > height - offsetY * 2 && currentPosY < height - offsetY) {
			//below max height
			Debug.Log("max height");
			powerupPosition.y = currentPosY - offsetY;
		}

		if (currentPosY > groundLevel + offsetY && currentPosY < groundLevel + offsetY * 2) {
			//above min height
			Debug.Log("min height");
			powerupPosition.y = currentPosY + offsetY;
		}

		powerupPosition.x = player.transform.position.x + powerupOffset;
		powerupPosition.z  = 0;
		prevPosY = currentPosY;
		return powerupPosition;
	}
}

