using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bossAttack : MonoBehaviour
{


	public enum attackTypes 
	{
		singleFire = 0,
		multipleShots = 1,
		spreadShot = 2,
		//superAttack = 3
	};

    #region Variables

    List<GameObject> attackList;
	attackTypes bossAttackTypes;


	float timeElapsed;
	float lastStateCheck;
	float lastAttackCheck;


	#region AttackTypeVariables

	//public float attackDelay = 5.0f;

	/// below variables deal with boss attack delays
	public float startGameDelay = 10.0f; //start of game delay
	public float shotDelay = 5.0f;       //delay between each shots
	public float shotSpeed = 5.0f; 		 //speed of shots

	//below variables deal with the number of projectiles for each attack type
	public int singleShotCount = 1;
	public int multipleShotCount = 5;
	public int spreadShotCount = 15;
	public int superAttackShotCount = 30;

	//below variables deal with the projectiles speed
	public int singleShotSpeed = 1;
	public int multipleShotSpeed = 3;
	public int spreadShotSpeed = 10;
	public int superAttackShotSpeed = 30;

	//below variables deal with delay between projectiles
	public float singleShotDelay = 1;
	public float multipleShotDelay = 2;
	public float spreadShotDelay = 1f; 
	public float superAttackShotDelay = 30;

    #endregion

	private int shotCount = 0; //number of shots allocated for specified attack
	private int currentCount = 0;  //number of shots boss has fired
	private int previousHealth;

	#endregion

	// Use this for initialization
	void Start ()
	{
		previousHealth = 1;
		getPrefabs ();
		bossAttackTypes = randomiseAttackType();
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

	private void getPrefabs ()
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
	/// Randomly picks which attack type (fire, plasma etc) to use
	/// </summary>
	private int randomiseAttack ()
	{
		int index = Random.Range(0, attackList.Count);
		return index;
	}

	private attackTypes randomiseAttackType()
	{
		int index = Random.Range(0, 3);
		attackTypes aType = (attackTypes)index;
		return aType;
	}

	/// <summary>
	/// Determines which attack the boss will use
	/// </summary>
	private void determineAttack ()
	{
		int playersHealth = (int)healthbar.GetHealthBarValue();

		if (previousHealth > playersHealth) { //player has been hit
			if (lastAttackCheck + shotDelay < timeElapsed) { //delay until random attack is picked
				System.Array A = System.Enum.GetValues(typeof(attackTypes));
				bossAttackTypes = (attackTypes)A.GetValue(Random.Range(0,A.Length));
			}
		}

		if (playersHealth == previousHealth) //not been hit
		if (lastAttackCheck + shotDelay < timeElapsed) { //time until attack is assigned
			//bossAttackTypes = attackTypes.superAttack;
		}

		previousHealth = playersHealth;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="fire"></param>
	private void spawnObject (bool fire)
	{
		int index = 0;
		//Debug.Log("Shot Delay: " + shotDelay);

		if (currentCount >= shotCount)
			bossAttackTypes = randomiseAttackType();

		Debug.Log("Shot Type: " + bossAttackTypes);

		if (currentCount <= shotCount && fire)
		if (lastStateCheck + shotDelay < timeElapsed) {
	
			Vector3 bossPos = transform.position;
			Vector3 fireSpawn = new Vector3 (bossPos.x - 1, bossPos.y, 0.0f);
			attackList [index].transform.localScale = new Vector3 (0.1f, 0.1f, 1); //transform scale of object
			GameObject obj;  
			

			if (bossAttackTypes == attackTypes.spreadShot)
					SpreadShotAttackSpawner(index, attackList[index], fireSpawn);
			else
					Instantiate(attackList[index], fireSpawn, Quaternion.identity); //spawn object


                if (currentCount == shotCount) //if attack has finished
				{
                    currentCount = 0;
                    determineAttack();
                }

                //if (lastStateCheck + attackDelay < timeElapsed)
                //if(currentCount == shotCount){ //if attack has finished
                //	currentCount = 0;
                //	determineAttack();
                //}

            currentCount++;
			lastStateCheck = timeElapsed;
		}
	}

	/// <summary>
	/// assigns appropriate values for each attack based on the attack type assigned
	/// </summary>
	private void CheckAttackTypes ()
	{
		switch (bossAttackTypes) {

		case attackTypes.singleFire:
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
		//case attackTypes.superAttack:
		//	shotCount = superAttackShotCount;
		//	shotSpeed = superAttackShotSpeed;
		//	shotDelay = superAttackShotSpeed;
		//	spawnObject(true);
		//	break;
		default:
				//determineAttack();
			break;
		}
	}

	/// <summary>
	/// Method for Spread Shot attack
	/// Changes the angles of the attack
	/// </summary>
	private Vector3 SpreadShotAttackSpawner(int index, GameObject obj, Vector3 pos)
	{
		Vector3 objectAngle = new Vector3();

		objectAngle = obj.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
		obj = Instantiate(attackList[index], pos, Quaternion.identity); //spawn object
		objectAngle = obj.transform.eulerAngles = new Vector3(0.0f, 0.0f, 45.0f);
		obj = Instantiate(attackList[index], pos, Quaternion.identity); //spawn object
		objectAngle = obj.transform.eulerAngles = new Vector3(0.0f, 0.0f, -45.0f);
		obj = Instantiate(attackList[index], pos, Quaternion.identity); //spawn object

		//obj.transform.position += transform.right * Time.deltaTime * shotSpeed;
		return objectAngle;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	private Vector3 SuperAttackSpawner()
    {

		return new Vector3();
    }
}
