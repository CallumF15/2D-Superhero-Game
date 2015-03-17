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
	List<int> aList, minList;
	List<GameObject> powerupList;

	float timeElapsed;
	float lastStateCheck = 0f;
	public float delay = 5.0f;
	
	// Use this for initialization
	void Start ()
	{
		aList = new List<int> ();
		minList = new List<int> ();
		powerupList = new List<GameObject> ();
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

	void getPrefabs(){

		//Rigidbody2D healthPickup = Resources.Load ("Powerups/Health") as Rigidbody2D;
		GameObject healthPickup = Resources.Load ("Health") as GameObject;
	
		if (healthPickup == null)
			Debug.Log ("NULL");
		else
			Debug.Log ("NOT NULL");

		//Rigidbody2D shieldPickup = Resources.Load ("IP3_2D_CF/IP3_2D/Assets/Models/Powerups/Shield") as Rigidbody2D;
		//Rigidbody2D healthPickup = Resources.Load ("healthpickup");

		GameObject blah = Instantiate(healthPickup, new Vector3(0,0,0), Quaternion.identity) as GameObject;
		//Rigidbody2D blah = Instantiate(healthPickup, new Vector3(0,0,0), Quaternion.identity) as Rigidbody2D;

	}

	void calculatePosition(){

				player = GameObject.FindGameObjectWithTag ("Player");
				Vector3 playerPos = player.transform.position;
				Vector3 prevPos;
		
				Debug.Log ("rotation" + player.transform.rotation.eulerAngles.z);
				powerObject.transform.position = playerPos;
				powerObject.transform.eulerAngles = new Vector3 (player.transform.eulerAngles.x, player.transform.eulerAngles.y, Angle); 

		if (playerPos > prevPos) {
			playerPos = player.transform.position;
		}
		
		//get Player Angle (between 45 and -45)
		//calculate Y height based on those values

	}

	void powerupSelector(){

		int index = Random.Range (0, numberOfPowerups - 1);
		aList.Add (index);

		//should arrange List in ascending/descending Order

		var q = from x in aList
			group x by x into g
				let count = g.Count()
				orderby count ascending
		select new {Value = g.Key, Count = count};

//		var list = aList;
//		var q = list.GroupBy(x => x)
//			.Select(g => new {Value = g.Key, Count = g.Count()})
//				.OrderByDescending(x=>x.Count);

		foreach (var x in q)
		{
			Debug.Log("Value: " + x.Value + " Count: " + x.Count);
			minList.Add(x.Value);
			//pick power up in here?
			//powerupList

			//int[][] value = new int[10][10];



			 //need to find lowest used number
			//GameObject obb = powerupList[index];

		}
	}


}

