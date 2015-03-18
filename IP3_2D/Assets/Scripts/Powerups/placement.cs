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

	List<int> aList;

	float timeElapsed;
	float lastStateCheck = 0f;
	float lastStateCheck2 = 0f;
	public float delay = 5.0f;

	private float currentPosY, prevPosY, currentPosX, prevPosX;

	// Use this for initialization
	void Start ()
	{
		aList = new List<int> ();
		powerArray = new List<GameObject> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		getPrefabs ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		timeElapsed = Time.time;
		currentPosX = player.transform.position.x;


		
		if (currentPosX > prevPosX) {
			if (lastStateCheck + delay < timeElapsed) {
				powerupSelector ();
				lastStateCheck = timeElapsed;
			}

			if(lastStateCheck2 + 1 < timeElapsed){
				int index = Random.Range (0, powerArray.Count );
				aList.Add (index);
				lastStateCheck2 = timeElapsed;

			}


			prevPosX = currentPosX;
		}
	}

	void getPrefabs ()
	{
		GameObject coinPickup = Resources.Load ("Powerups/coin") as GameObject;
		GameObject healthPickup = Resources.Load ("Powerups/Health") as GameObject;
		GameObject crystalPickup = Resources.Load ("Powerups/CrystalGreen") as GameObject;
		GameObject shieldPickup = Resources.Load ("Powerups/Health") as GameObject;

//		coinPickup.transform.rotation = Quaternion.Euler (new Vector3 (0, 90, 90));
//		healthPickup.transform.rotation = Quaternion.Euler (new Vector3 (90, 0, 0));
//		crystalPickup.transform.rotation = Quaternion.Euler (new Vector3 (270,0, 0));
//		shieldPickup.transform.rotation = Quaternion.Euler (new Vector3 (270, 0, 0));
	
		powerArray.Add (coinPickup);
		powerArray.Add (healthPickup);
		powerArray.Add (crystalPickup);
		powerArray.Add (shieldPickup);
	}
	
	/// <summary>
	/// Selects the least use power-up and spawns it
	/// </summary>
	void powerupSelector ()
	{


		var q = from x in aList
			group x by x into g
				let count = g.Count ()
				orderby count ascending
		select new {Value = g.Key, Count = count};

		int lastValue = 0;
		int heightestValue = 0;
		int lowestValue = 1;
		int select = 0;

		//coin 0
		//health 1
		//crystal 2
		//shield 3

		foreach (var x in q) {
			Debug.Log ("Value: " + x.Value + " Count: " + x.Count);

				Debug.Log("heightVal " + heightestValue);
				if(x.Count > heightestValue){
					heightestValue = x.Count;
					//select = x.Value;
				}
				
				Debug.Log("lowValu " + lowestValue);

			if(lowestValue < x.Count ){
					lowestValue = x.Count;
					select = x.Value;
				Debug.Log("select " + x.Value);
				}
		}
		//Debug.Log("select val " + select);
		spawnPowerup (select);
	}


	/// <summary>
	/// Spawns the powerup.
	/// </summary>
	public void spawnPowerup(int index)
	{
		GameObject selectedObject = powerArray [index];
		currentPosX = player.transform.position.x;
		Vector3 position = calcPowerupPosition ();


			//GameObject blah =  Instantiate (GameObject.CreatePrimitive (PrimitiveType.Cube), position, Quaternion.identity) as GameObject;
			GameObject blah =  Instantiate (selectedObject, position, selectedObject.transform.rotation) as GameObject;
			//blah.AddComponent<Rigidbody>();
			//blah.rigidbody.useGravity = false;
			blah.tag = "cubes";

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
				Debug.Log("up way");
				//if going up for 5 seconds, activate power-up spawn
				powerupPosition.y = currentPosY + 5;
				//lastStateCheck = timeElapsed;
				//prevPosY = currentPosY;
			}
		}

		if (currentPosY < prevPosY && currentPosY > groundLevel + 10.0f) {		//we are going down the way
			if (lastStateCheck + delay < timeElapsed) { 
				Debug.Log("down way");
				//if going up for 5 seconds, activate power-up spawn
				powerupPosition.y = currentPosY - 5;
				//lastStateCheck = timeElapsed;
				//prevPosY = currentPosY;
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

