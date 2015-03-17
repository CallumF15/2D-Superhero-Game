using UnityEngine;
using System.Collections;

public class bossAttack : MonoBehaviour
{
	public enum attackTypes
	{
		defaultstate,
		singleFire,
		multipleShots,
		spreadShot,
		superAttack
	}
	;

	public Rigidbody2D attackPrefab;
	Rigidbody2D attackInstance;
	attackTypes bossAttackTypes;
	Vector3 playerPos;
	float timeElapsed;
	float lastStateCheck = 0f;

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
	public int singleShotDelay = 1;
	public int multipleShotDelay = 2;
	public int spreadShotDelay = 2; 
	public int superAttackShotDelay = 30;

	float timeFrequency;
	bool canFire;
	private int shotCount = 0;
	private int currentCount = 0;

	Rigidbody2D currentAttack;
	
	// Use this for initialization
	void Start ()
	{
		bossAttackTypes = attackTypes.spreadShot;
	}
	
	// Update is called once per frame
	void Update ()
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
		CheckAttackTypes (); //replace with CheckAttackTypes();
	}

	/// <summary>
	/// Randomly picks which attack type to use
	/// </summary>
	void determineAttack ()
	{
		Rigidbody2D icePrefab = Resources.Load ("iceAttack") as Rigidbody2D;
		Rigidbody2D plasmaPrefab = Resources.Load ("plasmaAttack") as Rigidbody2D;
		Rigidbody2D firePrefab = Resources.Load ("fireAttack") as Rigidbody2D;

		System.Collections.Generic.List<Rigidbody2D> attackList = new System.Collections.Generic.List<Rigidbody2D> ();
		attackList.Add (icePrefab);
		attackList.Add (plasmaPrefab);
		attackList.Add (firePrefab);

		int index = Random.Range (0, attackList.Count);
		currentAttack = attackList [index];
	}
	
	void spawnObject (bool fire)
	{
		//if (currentCount >= shotCount)
		//	currentCount = 0;
		
		if (currentCount <= shotCount && fire)
		if (lastStateCheck + shotDelay < timeElapsed) {
	
			Vector3 bossPos = transform.position;
			Vector3 fireSpawn = new Vector3 (bossPos.x - 1, bossPos.y , 0.0f);
		
			attackInstance = Instantiate (attackPrefab, fireSpawn, Quaternion.identity) as Rigidbody2D;
			attackInstance.transform.localScale = new Vector3(0.1f, 0.1f, 1);

			if(bossAttackTypes == attackTypes.spreadShot)
				shotCountAttack();
			//else
				//attackInstance.velocity = (new Vector2 (-shotSpeed, 0));
			
			currentCount++;
			lastStateCheck = timeElapsed;
		} else
			canFire = false;
	}
	
	void CheckAttackTypes ()
	{
			switch (bossAttackTypes) {

			case attackTypes.singleFire:
				Debug.Log("singleFire");
				shotCount = singleShotCount;
				shotSpeed = singleShotSpeed;
				shotDelay = singleShotDelay;
				spawnObject(true);
				break;

			case attackTypes.multipleShots:
			    shotCount = multipleShotCount;
				shotSpeed = multipleShotSpeed;
				shotDelay = multipleShotDelay;
				spawnObject(true);
				break;
			case attackTypes.spreadShot:
				shotCount = spreadShotCount;
				shotSpeed = spreadShotSpeed;
			    shotDelay = spreadShotDelay;
				spawnObject (true);

				break;
			case attackTypes.superAttack:
				shotCount = 20;

				Vector2 pos = transform.position;
				Vector2 screenPos = camera.WorldToScreenPoint(pos);
				int offset = 10;

				for(int i = 0; i <= 4; i++){

					int width = (Screen.width / i) - offset;
					int height = (Screen.height / i) - offset;

					Vector2 newPos = camera.ScreenToWorldPoint(new Vector3(width, height, 0));
					transform.position = newPos;

					attackInstance.transform.position = new Vector3(-superAttackShotSpeed, 0, 0);
				}

				break;
			default:
				break;
		}
	}

	void shotCountAttack(){
		float shotSpread = shotCount / 3;

		if (currentCount <= shotSpread) {
			attackInstance.transform.Rotate(0, 0, 0);
		}
		
		if (currentCount < (shotSpread * 2) && currentCount > shotSpread) {
			attackInstance.transform.Rotate(0, 0, 45);
			attackInstance.rotation = 45;
			//attackInstance.transform.position -= transform.right * Time.deltaTime * shotSpeed;
		}
		
		if (currentCount > (shotSpread * 2) && currentCount <= spreadShotCount) {
			//attackInstance.transform.Rotate(0, 0, -45);
			attackInstance.rotation = -45;
			attackInstance.velocity = transform.right * spreadShotSpeed;
		}

		//attackInstance.velocity = -transform.TransformDirection(Vector3.right) * shotSpeed;
		attackInstance.velocity = -transform.right * spreadShotSpeed;

		//attackInstance.transform.rigidbody2D.AddForce (-transform.forward * Time.deltaTime * shotSpeed, ForceMode2D.Impulse);
		//attackInstance.rigidbody2D.velocity = -transform.forward * Time.deltaTime * spreadShotSpeed;
		//attackInstance.transform.position = -transform.right * Time.deltaTime * spreadShotSpeed;
		//attackInstance.rigidbody2D.AddForce(attackInstance.transform.right * Time.deltaTime * spreadShotSpeed);
	}
}
