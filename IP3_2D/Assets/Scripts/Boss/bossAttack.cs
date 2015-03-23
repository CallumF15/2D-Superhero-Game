using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bossAttack : MonoBehaviour
{
	public enum attackTypes 
	{
		defaultstate = 0,
		singleFire = 1,
		multipleShots = 2,
		spreadShot = 3,
		superAttack = 4
	};

	List<GameObject> attackList;
	public GameObject attackPrefab;
	Rigidbody2D attackInstance;
	attackTypes bossAttackTypes;
	Vector3 playerPos;
	float timeElapsed;
	float lastStateCheck;
	float lastAttackCheck;
	public float attackDelay = 5.0f;

	/// below variables deal with boss attack delays
	public float startGameDelay = 10.0f;  //start of game delay
	public float shotDelay = 5.0f;       //delay between each shots
	public float shotSpeed = 5.0f; 		  //speed of shots

	//below variables deal with the number of projectiles for each attack type
	public int singleShotCount = 1;
	public int multipleShotCount = 5;
	public int spreadShotCount = 15; //must be dividle by 3
	public int superAttackShotCount = 30;

	//below variables deal with the projectiles speed
	public int singleShotSpeed = 1;
	public int multipleShotSpeed = 3;
	public int spreadShotSpeed = 10;
	public int superAttackShotSpeed = 30;

	//below variables deal with delay between projectiles
	public float singleShotDelay = 1;
	public float multipleShotDelay = 2;
	public float spreadShotDelay = 2;
	public float superAttackShotDelay = 30;
	float timeFrequency;
	private int shotCount = 0;
	private int currentCount = 0;

	// Use this for initialization
	void Start ()
	{
		getPrefabs ();
		bossAttackTypes = attackTypes.spreadShot;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		Debug.Log ("attack type" + bossAttackTypes);
		timeElapsed = Time.time;
		StartCoroutine (delayTimer (startGameDelay));
	}

	/// <summary>
	/// Game has to wait so many seconds before boss will shoot (could change to till boss appears)
	/// </summary>
	IEnumerator delayTimer (float delayTimer)
	{
		yield return new WaitForSeconds (delayTimer);
		CheckAttackTypes (); 
	}

	void getPrefabs ()
	{
		attackList = new List<GameObject> ();
		GameObject icePrefab = Resources.Load ("bossAttacks/iceAttack") as GameObject;
		GameObject plasmaPrefab = Resources.Load ("bossAttacks/plasmaAttack") as GameObject;
		GameObject firePrefab = Resources.Load ("bossAttacks/fireAttack") as GameObject;

		attackList.Add (icePrefab);
		attackList.Add (plasmaPrefab);
		attackList.Add (firePrefab);
	}

	/// <summary>
	/// Randomly picks which attack type to use
	/// </summary>
	int determineAttack ()
	{
		int index = Random.Range (0, attackList.Count);
		return index;
	}

	void determineAttack2 ()
	{
		int Health = playerHealth.getHealth;
		int previousHealth = 0;

		if (previousHealth > Health) { //been hit
			if (lastAttackCheck + shotDelay < timeElapsed) { //delay until random attack is picked
				System.Array A = System.Enum.GetValues(typeof(attackTypes));
				bossAttackTypes = (attackTypes)A.GetValue(Random.Range(0,A.Length));
			}
		}

		if (Health == previousHealth) //not been hit
		if (lastAttackCheck + shotDelay < timeElapsed) { //time until attack is assigned
			bossAttackTypes = attackTypes.superAttack;
		}

		previousHealth = Health;
	}

	void spawnObject (bool fire)
	{
		int index = 0;
		//if (currentCount >= shotCount)
		//	currentCount = 0;

		if (currentCount >= shotCount)
			index = determineAttack ();
		
		if (currentCount <= shotCount && fire)
		if (lastStateCheck + shotDelay < timeElapsed) {
	
			Vector3 bossPos = transform.position;
			Vector3 fireSpawn = new Vector3 (bossPos.x - 1, bossPos.y, 0.0f);

			attackList [index].transform.localScale = new Vector3 (0.1f, 0.1f, 1);
			Instantiate (attackList [index], fireSpawn, Quaternion.identity);

			if (bossAttackTypes == attackTypes.spreadShot)
				shotCountAttack (index);

			if(lastStateCheck + attackDelay < timeElapsed)
			if(currentCount == shotCount){
				currentCount = 0;
				determineAttack2();
			}
						
			currentCount++;
			lastStateCheck = timeElapsed;
		}
	}
	
	void CheckAttackTypes ()
	{
		switch (bossAttackTypes) {

		case attackTypes.singleFire:
			;
			shotCount = singleShotCount;
			shotSpeed = singleShotSpeed;
			shotDelay = singleShotDelay;
			spawnObject (true);
			break;

		case attackTypes.multipleShots:
			shotCount = multipleShotCount;
			shotSpeed = multipleShotSpeed;
			shotDelay = multipleShotDelay;
			spawnObject (true);
			break;
		case attackTypes.spreadShot:
			shotCount = spreadShotCount;
			shotSpeed = spreadShotSpeed;
			shotDelay = spreadShotDelay;
			spawnObject (true);

			break;
		case attackTypes.superAttack:
			shotCount = superAttackShotCount;
			shotSpeed = superAttackShotSpeed;
			shotDelay = superAttackShotSpeed;

				//call method
			break;
		default:
			break;
		}
	}

	void shotCountAttack (int index)
	{
		float shotSpread = shotCount / 3;

		if (currentCount <= shotSpread) {
			attackList [index].transform.Rotate (0, 0, 0);
		}
		
		if (currentCount < (shotSpread * 2) && currentCount > shotSpread) {
			//attackInstance.transform.Rotate(0, 0, 45);
			attackList [index].transform.Rotate (0, 0, 45);
			//attackInstance.transform.position -= transform.right * Time.deltaTime * shotSpeed;
		}
		
		if (currentCount > (shotSpread * 2) && currentCount <= spreadShotCount) {
			//attackInstance.transform.Rotate(0, 0, -45);
			attackList [index].transform.Rotate (0, 0, -45);
			//attackInstance.velocity = transform.right * spreadShotSpeed;
		}
		transform.parent.position += transform.right * Time.deltaTime * shotSpeed;
	}

	void superCountAttack()
	{

	}
}
