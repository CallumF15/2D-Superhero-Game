using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class placement : MonoBehaviour
{
	GameObject player, powerObject;
	public float distanceBetweenPowerup = 30.0f;
	private int numberOfPowerups = 5;
	private int Angle = 45;

	List<int> aList;
	List<Rigidbody2D> powerupList;

	float timeElapsed;
	float lastStateCheck = 0f;
	public float delay = 5.0f;

	private float currentPosY, prevPosY;
	private bool canSpawn;

	// Use this for initialization
	void Start ()
	{
		aList = new List<int> ();
		powerupList = new List<Rigidbody2D> ();
		//getPrefabs ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Need to put a delay on this
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

		foreach (var x in q) {
			Debug.Log ("Value: " + x.Value + " Count: " + x.Count);

			if(x.Count > lastValue)
			{
				if(x.Count > heightestValue)
					heightestValue = x.Count;

				if(x.Count < lowestValue)
					lowestValue = x.Count; 

				lastValue = x.Count;
			}
		}
		spawnPowerup (lowestValue);
	}

	/// <summary>
	/// Spawns the powerup.
	/// </summary>
	public void spawnPowerup(int index)
	{
		Rigidbody2D selectedObject = powerupList [index];
		Vector3 position = calcPowerupPosition ();
		Instantiate (selectedObject, position, Quaternion.identity);
	}

	/// <summary>
	/// Calculates the powerup position.
	/// </summary>
	Vector3 calcPowerupPosition ()
	{
		Vector3 powerupPosition = new Vector3 ();
		float distance = calculateDistance ();
		playerHeight heightLimit = new playerHeight ();
		float height = heightLimit.playerHeightLimit;
		float groundLevel = 0;
		float offsetY = 20f;


		currentPosY = player.transform.position.y;

		if (currentPosY > prevPosY) {
			if (currentPosY < height - offsetY) {
				//below max height
				powerupPosition.y = currentPosY - offsetY;
			}

			//note: need to get time elapsed going up & down
			//we are going up the way

			if (lastStateCheck + delay < timeElapsed) { 
				//if going up for 5 seconds, activate power-up spawn
				canSpawn = true;
				lastStateCheck = timeElapsed;
				prevPosY = currentPosY;
			}
		}

		if (currentPosY < prevPosY) {	
			if (currentPosY > groundLevel + offsetY) {
				//above min height
				powerupPosition.y = currentPosY + offsetY;
			}
			//we are going down the way
			powerupPosition.y = currentPosY + 10;
		}


		if (canSpawn == true) {
			//spawn prefab
			//call method pass position Vector3 parameter
		}


		prevPosY = currentPosY;

		return powerupPosition;
	}

	/// <summary>
	/// Calculates the distance per second
	/// </summary>
	float calculateDistance ()
	{
		Movement blah = new Movement ();
		float speed = blah.flySpeed;
		float distance = speed * Time.deltaTime;
		return distance;
	}


}

